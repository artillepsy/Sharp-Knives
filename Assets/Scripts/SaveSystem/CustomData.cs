using System.Collections.Generic;
using Core;
using Scriptable;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Класс, содержащий сохраняемые значения
    /// </summary>
    [System.Serializable]
    public class UserData
    {
        public int AppleCount;
        public int HighScore;
        public int EquippedKnifeId;
        public List<int> UnlockedKniveIds;
        public float Volume = 0.5f;
        public bool Vibration = true;
        
        public int WinCount;
        public int BossHP;

        /// <summary>
        /// конструктор, в котором можно задать начальное количество яблок,
        /// открытых ножей, а также нож, который будет экипирован
        /// </summary>
        public UserData(int appleCount, int equippedKnifeId, List<int> unlockedKnives)
        {
            AppleCount = appleCount;
            EquippedKnifeId = equippedKnifeId;
            UnlockedKniveIds = unlockedKnives;
            HighScore = 0;
            Volume = 0.5f;
            Vibration = true;
            WinCount = 0;
            BossHP = 100;
        }
    }
    /// <summary>
    /// Базовый класс для тех, которые предоставляют ограниченный доступ к данным,
    /// хранящимся в файле
    /// </summary>
    public class AbstractData
    {
        protected UserData _userData;
        public AbstractData(UserData userData) => _userData = userData;
    }
    /// <summary>
    /// Класс, хранящий данные ножа
    /// </summary>
    public class KnifeData : AbstractData
    {
        public Sprite CurrentSprite;
        public KnifeData(UserData userData) : base(userData)
        {
        }
        /// <summary>
        /// Метод, сохраняющий спрайт экипированного ножа
        /// </summary>
        public void Equip(KnifeShopItem item) => CurrentSprite = item.KnifeSprite;
    }
    /// <summary>
    /// Класс, предоставляющий доступ к настройкам аудио
    /// </summary>
    public class SoundData : AbstractData
    {
        public float Volume => _userData.Volume;
        public bool Vibration => _userData.Vibration;
        public SoundData(UserData userData) : base(userData)
        {
        }
        /// <summary>
        /// Метод, сохраняемый значения аудио и вибрации в файл. Вызывает
        /// события изменения настроек
        /// </summary>
        public void SetVolumeSettings(float volume, bool vibration)
        {
            _userData.Volume = volume;
            _userData.Vibration = vibration;
            SaveSystem.Save(_userData);
            Events.OnSettingsChange?.Invoke();
        }
    }
    /// <summary>
    /// Класс, предоставляющий доступ к данным о ножах в файле и
    /// к данным о ножах из магазина
    /// </summary>
    public class ShopData : AbstractData
    {
        public List<KnifeShopItem> Knives;
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;
        public int EquippedId => _userData.EquippedKnifeId;
        public ShopData(UserData userData) : base(userData)
        {
        }
        /// <summary>
        /// Метод открытия ножика. Добавляет его в список разблокированных, сохраняет
        /// информацию и вызывает соответствующее событие
        /// </summary>
        public void Unlock(KnifeShopItem item)
        {
            if(!item.IsBossDrop) _userData.AppleCount -= item.Cost;
            _userData.UnlockedKniveIds.Add(item.Id);
            Events.OnUnlock?.Invoke();
            SaveSystem.Save(_userData);
        }
        /// <summary>
        /// Метод, открывающий ножик за победу над боссом. Вызывает события при открытии
        /// и сохраняет данные в файл
        /// </summary>
        public void UnlockBossKnife()
        {
            var lockedBossKnives = new List<KnifeShopItem>();
            foreach (var knife in Knives)
            {
                if (_userData.UnlockedKniveIds.Contains(knife.Id)) continue;
                if (!knife.IsBossDrop) continue;
                lockedBossKnives.Add(knife);
            }
            if (lockedBossKnives.Count == 0) return;
            var item = lockedBossKnives[Random.Range(0, lockedBossKnives.Count)];
            _userData.UnlockedKniveIds.Add(item.Id);
            Events.OnUnlock?.Invoke();
            SaveSystem.Save(_userData);
        }
        /// <summary>
        /// Метод, меняющий идентификатор экипированного ножа в файле и
        /// вызывающий соответствующее событие
        /// </summary>
        public void Equip(KnifeShopItem item)
        {
            _userData.EquippedKnifeId = item.Id;
            SaveSystem.Save(_userData);
            Events.OnEquip?.Invoke(item);
        }
    }
    /// <summary>
    /// Класс, предоставляющий доступ к игровму счету
    /// </summary>
    public class ScoreData: AbstractData
    {
        public int WinCount => _userData.WinCount;
        public int AppleCount => _userData.AppleCount;
        public int HighScore
        {
            get
            {
                return _userData.HighScore;
            }
            set
            {
                _userData.HighScore = value;
            }
        }

        public int BossHP => _userData.BossHP;
        public int CurrentScore;

        public ScoreData(UserData userData) : base(userData)
        {
        }
        
        public void IncrementApples() => _userData.AppleCount += 2;

        public void ResetWinCount() => _userData.WinCount = 0;
        public void IncrementWins() => _userData.WinCount++;
        /// <summary>
        /// Метод нанесения боссу урона. Если босс побежден, вызывается
        /// соответствующее событие здоровье босса восстанавливается
        /// </summary>
        public void DamageBoss(int damage)
        {
            if (_userData.BossHP <= damage)
            {
                Events.OnDefeatBoss?.Invoke();
                _userData.BossHP = 100;
                return;
            }
            _userData.BossHP -= damage;
            SaveSystem.Save(_userData);
        }
    }
}