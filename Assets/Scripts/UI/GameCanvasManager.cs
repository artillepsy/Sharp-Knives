using System.Linq;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameCanvasManager : AbstractCanvasManager
    {
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
            Events.OnFailGame.AddListener( () => NotifyAll(CanvasType.Fail));
        }
        private void Start() => NotifyAll(CanvasType.Game);
        private void LoadMenuLevel() => SceneManager.LoadSceneAsync("Menu");
    }
}