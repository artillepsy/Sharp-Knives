using Knife;
using Log;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Management
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string levelName = "Game";
        [SerializeField] private float newSceneLoadTIme = 3f;
        
        private int _knifeCount;
        public static readonly UnityEvent OnWinGame = new UnityEvent();
        public void SetKnifeCount(int count)
        {
            _knifeCount = count;
        }

        private void OnEnable()
        {
            KnifeStateController.OnDrop.AddListener(OnGameOver);
            LogStickness.OnKnifeStick.AddListener(ReduceKnifeCount);
        }

        private void ReduceKnifeCount()
        {
            _knifeCount--;
            if(_knifeCount == 0) OnWin();
        }

        private void OnGameOver()
        {
            SceneManager.LoadScene(levelName);
            Vibration.Vibrate(2000);
        }
        private void OnWin()
        {
            Invoke(nameof(LoadScene), newSceneLoadTIme);
            OnWinGame?.Invoke();
            Vibration.Vibrate(2000);
        }


        private void LoadScene()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}