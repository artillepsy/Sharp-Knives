using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class GameManager : MonoBehaviour
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
           Invoke(nameof(LoadGameOverScreen), newSceneLoadTIme);
           Vibration.Vibrate(400);
        }
        private void OnWin()
        {
            Invoke(nameof(LoadScene), newSceneLoadTIme);
            Events.OnWinGame?.Invoke();
            Vibration.Vibrate(400);
        }

        private void LoadGameOverScreen() => Events.OnFailGame?.Invoke();
        private void LoadScene()
        {
            SceneManager.LoadSceneAsync(levelName);
        }
    }
}