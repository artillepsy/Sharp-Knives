using System.Linq;
using Core;
using Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Game
{
    public class GameCanvasManager : AbstractCanvasManager, IOnLevelLoad
    {
        [SerializeField] private float bossStartCanvasShowTime = 3f;
        
        private bool _isBoss = false;
        public void OnLevelLoad(Level level)
        {
            _isBoss = level.Log.Settings.IsBoss;

            if (_isBoss)
            {
                NotifyAll(CanvasType.BossLevelStart, false);
                Invoke(nameof(HideStartBossCanvas), bossStartCanvasShowTime);
            }
            else NotifyAll(CanvasType.Game, false);
        }

        public void OnClickPause()
        {
           // Time.timeScale = 0f;
            NotifyAll(CanvasType.Pause);
        }

        public void OnClickRestart()
        {
            //NotifyAll(CanvasType.Game);
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnClickResume()
        {
           // Time.timeScale = 1f;
            NotifyAll(CanvasType.Game);
        }

        public void OnClickMainMenu()
        {
           // Time.timeScale = 1f;
           // SceneManager.LoadSceneAsync("Menu");
           Invoke(nameof(LoadMenuLevel), changeDelayInSeconds);
        }

        private void OnEnable()
        {
            _subs = FindObjectsOfType<MonoBehaviour>().OfType<IOnCanvasChange>().ToList();
            Events.OnFailGame.AddListener(() => NotifyAll(CanvasType.Fail));
            Events.OnWinGame.AddListener(() =>
            {
                if (_isBoss) NotifyAll(CanvasType.BossLevelEnd);
            });
        }
        private void HideStartBossCanvas() => NotifyAll(CanvasType.Game);
        private void LoadMenuLevel() => SceneManager.LoadSceneAsync("Menu");
        
    }
}