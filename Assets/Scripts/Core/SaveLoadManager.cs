using UnityEngine;

namespace Core
{
    public class SaveLoadManager : MonoBehaviour
    {
        private UserData _userData;
        public int AppleCount => _userData.AppleCount;
        private int _currentScore = 0;
        public int CurrentScore => _currentScore;

        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(() =>_userData.AppleCount++);
            Events.OnKnifeHit.AddListener(() => _currentScore++);
            Events.OnWinGame.AddListener(() => SaveLoadSystem.Save(_userData));
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
        }

        private void Start()
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

        /*
        public void OnClickSave()
        {
            SaveLoadSystem.Save(_userData);
        }

        public void OnClickLoad()
        {
            _userData = SaveLoadSystem.Load();
        }

        public void OnClickChangeValues()
        {
            _userData.AppleCount++;
            _userData.HighScore++;
        }

        public void OnClickCheck()
        {
            Debug.Log(_userData.AppleCount);
            Debug.Log(_userData.HighScore);
        }*/
    }
}