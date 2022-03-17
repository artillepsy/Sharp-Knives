using System.Collections.Generic;
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
        public int EquippedKnifeId => _userData.EquippedKnifeId;
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
        }
    }
    public class ShopData : AbstractData
    {
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;
        public ShopData(UserData userData) : base(userData)
        {
        }
        
        public void Unlock(int id, int cost)
        {
            _userData.AppleCount -= cost;
            _userData.UnlockedKniveIds.Add(id);
            SaveSystem.Save(_userData);
        }

        public void Equip(KnifeShopItem item)
        {
            _userData.EquippedKnifeId = item.Id;
            SaveSystem.Save(_userData);
        }
    }
    public class ScoreData: AbstractData
    {
        public int WinCount => _userData.WinCount;
        public int AppleCount => _userData.AppleCount;
        public int HighScore => _userData.HighScore;
        public int CurrentScore;

        public ScoreData(UserData userData) : base(userData)
        {
        }

        public void IncrementApples() => _userData.AppleCount++;
        public void ResetWinCound() => _userData.WinCount = 0;
        public void IncrementWins() => _userData.WinCount++;

        public void DamageBoss(int damage)
        {
            if (_userData.BossHP <= damage)
            {
                Debug.Log("boss defeated");
                Debug.Log("Unlock Random Knife");
                _userData.BossHP = 100;
                return;
            }
            _userData.BossHP -= damage;
            SaveSystem.Save(_userData);
        }
    }
}