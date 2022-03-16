using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class SaveLoadManager : MonoBehaviour
    {
        private UserData _userData;
        private int _currentScore = 0;
        private int _winCount = 0;
        public int WinCount => _winCount;
        public int AppleCount => _userData.AppleCount;
        public int CurrentScore => _currentScore;
        public int HighScore => _userData.HighScore;
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;

        public void SaveProgress() => SaveLoadSystem.Save(_userData);
        public void LoadProgress() => _userData = SaveLoadSystem.Load();
        public void Unlock(int id, int cost)
        {
            _userData.AppleCount -= cost;
            _userData.UnlockedKniveIds.Add(id);
            SaveProgress();
        }

        public void Equip(int id)
        {
            _userData.CurrentKnifeId = id;
            SaveProgress();
        }

        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(() =>_userData.AppleCount++);
            Events.OnKnifeHit.AddListener(() => _currentScore++);
            Events.OnWinGame.AddListener(() => SaveLoadSystem.Save(_userData));
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
        }

        private void Awake()
        {
            Test_ClearProgress();
            _userData = SaveLoadSystem.Load();
            if (_userData != null) return;
            _userData = new UserData(0, 0, 1, new List<int>(){1});
        }

        private void Test_ClearProgress()
        {
            _userData = new UserData(100, 0, 1, new List<int>(){1});
            SaveLoadSystem.Save(_userData);
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
            SaveLoadSystem.Save(_userData);
        }
    }
}