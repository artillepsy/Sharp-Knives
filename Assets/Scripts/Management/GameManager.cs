using Core;
using LevelSettings;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class GameManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private string levelName = "Game";
        [SerializeField] private float newSceneLoadTime = 3f;
        [SerializeField] private float afterBossWinLoadTime = 6f;
        private SaveManager _saveManager;
        private int _knifeCount;
        private Level _level;
        private bool _bossLevel = false;
        public void SetKnifeCount(int count) => _knifeCount = count;
        public void OnLevelLoad(Level level)
        {
            _level = level;
            _bossLevel = level.Log.Custom.IsBoss;
        }
        public void LoadNewLevel() => SceneManager.LoadSceneAsync(levelName);
        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
            Events.OnKnifeHit.AddListener(OnKnifeHit);
            Events.OnAppleHit.AddListener( () =>_saveManager.Score.IncrementApples());
        }

        private void Start() => _saveManager = FindObjectOfType<SaveManager>();
        private void OnKnifeHit()
        {
            _knifeCount--;
            _saveManager.Score.CurrentScore++;
            if(_knifeCount == 0) OnWin();
        }

        private void OnKnifeDrop()
        {
           Invoke(nameof(LoadGameOverScreen), newSceneLoadTime);
           if (_saveManager.Score.HighScore < _saveManager.Score.CurrentScore)
           {
               _saveManager.Score.HighScore = _saveManager.Score.CurrentScore;
           }
           _saveManager.Score.ResetWinCount();
           _saveManager.Score.CurrentScore = 0;
           _saveManager.Save();
        }
        private void OnWin()
        {
            var delay = _bossLevel ? afterBossWinLoadTime : newSceneLoadTime;
            Invoke(nameof(LoadNewLevel), delay);
            Debug.Log(delay);
            if (_bossLevel)
            {
                _saveManager.Score.DamageBoss(_level.Log.Custom.Boss.DamageAtDestroy);
            }
            
            _saveManager.Score.IncrementWins();
            _saveManager.Save();
            Events.OnWinGame?.Invoke();
        }
        private void LoadGameOverScreen() => Events.OnFailGame?.Invoke();
        
    }
}