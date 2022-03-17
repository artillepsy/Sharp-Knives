using System.Collections.Generic;
using Core;
using Scriptable;
using UI;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private UserData _userData;
        private int _currentScore = 0;
        private int _winCount = 0;
        private Sprite _currentKnifeSprite;
        public Sprite CurrentKnifeSprite => _currentKnifeSprite;
        public float Volume => _userData.Volume;
        public bool Vibration => _userData.Vibration;
        public int EquippedKnifeId => _userData.CurrentKnifeId;
        public int WinCount => _winCount;
        public int AppleCount => _userData.AppleCount;
        public int CurrentScore => _currentScore;
        public int HighScore => _userData.HighScore;
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;

        public void SaveProgress() => SaveSystem.Save(_userData);
        public void LoadProgress() => _userData = SaveSystem.Load();

        public void SetVolumeSettings(float volume, bool vibration)
        {
            _userData.Volume = volume;
            _userData.Vibration = vibration;
            SaveSystem.Save(_userData);
        }
        
        public void Unlock(int id, int cost)
        {
            _userData.AppleCount -= cost;
            _userData.UnlockedKniveIds.Add(id);
            SaveProgress();
        }

        public void Equip(KnifeShopItem item)
        {
            _userData.CurrentKnifeId = item.Id;
            _currentKnifeSprite = item.KnifeSprite;
            SaveProgress();
        }
        

        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(() =>_userData.AppleCount++);
            Events.OnKnifeHit.AddListener(() => _currentScore++);
            Events.OnWinGame.AddListener(() => SaveSystem.Save(_userData));
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
        }

        private void Awake()
        {
            Test_ClearProgress();
            _userData = SaveSystem.Load();
            if (_userData != null) return;
            _userData = new UserData(0, 0, 1, new List<int>(){1});
        }

        private void Start()
        {
            foreach (var knifeItem in FindObjectOfType<Shop>(true).KnifeItems)
            {
                if (knifeItem.Id != _userData.CurrentKnifeId) continue;
                _currentKnifeSprite = knifeItem.KnifeSprite;
            }
        }

        private void Test_ClearProgress()
        {
            _userData = new UserData(100, 0, 1, new List<int>(){1});
            SaveSystem.Save(_userData);
        }

        private void OnKnifeDrop()
        {
            Debug.Log(_currentScore);
            if (_userData.HighScore < _currentScore)
            {
                _userData.HighScore = _currentScore;
                Debug.Log("high score: "+ _userData.HighScore);
            }

            _winCount = 0;
            _currentScore = 0;
            SaveSystem.Save(_userData);
        }
    }
}