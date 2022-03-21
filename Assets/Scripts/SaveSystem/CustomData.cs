using System.Collections.Generic;
using Core;
using Scriptable;
using UnityEngine;

namespace SaveSystem
{
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
    public class AbstractData
    {
        protected UserData _userData;
        public AbstractData(UserData userData) => _userData = userData;
    }
    public class KnifeData : AbstractData
    {
        public Sprite CurrentSprite;
        public KnifeData(UserData userData) : base(userData)
        {
        }
        
        public void Equip(KnifeShopItem item) => CurrentSprite = item.KnifeSprite;
    }
    public class SoundData : AbstractData
    {
        public float Volume => _userData.Volume;
        public bool Vibration => _userData.Vibration;
        public SoundData(UserData userData) : base(userData)
        {
        }
        
        public void SetVolumeSettings(float volume, bool vibration)
        {
            _userData.Volume = volume;
            _userData.Vibration = vibration;
            SaveSystem.Save(_userData);
            Events.OnSettingsChange?.Invoke();
        }
    }
    public class ShopData : AbstractData
    {
        public List<KnifeShopItem> Knives;
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;
        public int EquippedId => _userData.EquippedKnifeId;
        public ShopData(UserData userData) : base(userData)
        {
        }
        
        public void Unlock(KnifeShopItem item)
        {
            if(!item.IsBossDrop) _userData.AppleCount -= item.Cost;
            _userData.UnlockedKniveIds.Add(item.Id);
            Events.OnUnlock?.Invoke();
            SaveSystem.Save(_userData);
        }

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

        public void Equip(KnifeShopItem item)
        {
            _userData.EquippedKnifeId = item.Id;
            SaveSystem.Save(_userData);
            Events.OnEquip?.Invoke(item);
        }
    }
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