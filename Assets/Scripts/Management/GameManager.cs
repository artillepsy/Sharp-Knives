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
        [SerializeField] private float newSceneLoadTIme = 3f;
        private SaveManager _saveManager;
        private ShopManager _shopManager;
        private int _knifeCount;
        private bool _bossLevel = false;
        
        public void SetKnifeCount(int count) => _knifeCount = count;
        public void OnLevelLoad(Level level) => _bossLevel = level.Log.Custom.IsBoss;
        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
            Events.OnKnifeHit.AddListener(OnKnifeHit);
            Events.OnAppleHit.AddListener( () =>_saveManager.Score.IncrementApples());
        }

        private void Start()
        {
            _shopManager = FindObjectOfType<ShopManager>();
            _saveManager = FindObjectOfType<SaveManager>();
        }

        private void OnKnifeHit()
        {
            _knifeCount--;
            _saveManager.Score.CurrentScore++;
            if(_knifeCount == 0) OnWin();
        }

        private void OnKnifeDrop()
        {
           Invoke(nameof(LoadGameOverScreen), newSceneLoadTIme);
           if (_saveManager.Score.HighScore < _saveManager.Score.CurrentScore)
           {
               _saveManager.Score.HighScore = _saveManager.Score.CurrentScore;
               Debug.Log("high score: "+ _saveManager.Score.HighScore);
           }
           if(_bossLevel)
           
           _saveManager.Score.ResetWinCount();
           _saveManager.Score.CurrentScore = 0;
           _saveManager.Save();
        }
        private void OnWin()
        {
            Invoke(nameof(LoadScene), newSceneLoadTIme);
            _saveManager.Score.IncrementWins();
            _saveManager.Save();
            Events.OnWinGame?.Invoke();
        }
        private void LoadGameOverScreen() => Events.OnFailGame?.Invoke();
        private void LoadScene()
        {
            SceneManager.LoadSceneAsync(levelName);
        }

        
    }
}