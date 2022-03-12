using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string levelName = "Game";
        [SerializeField] private float newSceneLoadTIme = 3f;
        
        private int _knifeCount;
        
        public void SetKnifeCount(int count)
        {
            _knifeCount = count;
        }

        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(OnGameOver);
            Events.OnKnifeHit.AddListener(ReduceKnifeCount);
        }

        private void ReduceKnifeCount()
        {
            _knifeCount--;
            if(_knifeCount == 0) OnWin();
        }

        private void OnGameOver()
        {
            Invoke(nameof(LoadScene), newSceneLoadTIme);
            Vibration.VibratePeek();
        }
        private void OnWin()
        {
            Invoke(nameof(LoadScene), newSceneLoadTIme);
            Events.OnWinGame?.Invoke();
            Vibration.VibratePeek();
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}