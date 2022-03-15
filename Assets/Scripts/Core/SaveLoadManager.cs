using UnityEngine;

namespace Core
{
    public class SaveLoadManager : MonoBehaviour
    {
        private UserData _userData;
        private int _currentScore = 0;
        public int AppleCount => _userData.AppleCount;
        public int CurrentScore => _currentScore;
        public int HighScore => _userData.HighScore;

        public void SaveProgress() => SaveLoadSystem.Save(_userData);
        public void LoadProgress() => _userData = SaveLoadSystem.Load();
        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(() =>_userData.AppleCount++);
            Events.OnKnifeHit.AddListener(() => _currentScore++);
            Events.OnWinGame.AddListener(() => SaveLoadSystem.Save(_userData));
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
        }

        private void Awake()
        {
            _userData = SaveLoadSystem.Load();
            if (_userData != null) return;
            _userData = new UserData(0, 0, 0, new int[] {0});
        }

        private void OnKnifeDrop()
        {
            Debug.Log(_currentScore);
            if (_userData.HighScore < _currentScore)
            {
                _userData.HighScore = _currentScore;
                Debug.Log("high score: "+ _userData.HighScore);
            }
            _currentScore = 0;
            SaveLoadSystem.Save(_userData);
        }
    }
}