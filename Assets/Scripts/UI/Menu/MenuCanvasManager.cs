using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    /// <summary>
    /// Менеджер окон для главного меню
    /// </summary>
    public class MenuCanvasManager : AbstractCanvasManager
    {
        private void Start()
        {
            _subs = FindObjectsOfType<MonoBehaviour>(true).OfType<IOnCanvasChange>().ToList();
            NotifyAll(CanvasType.MainMenu, false);
        }
        public void OnClickStart()
        {
            Invoke(nameof(LoadGameLevel), changeDelayInSeconds);
            NotifyAll(CanvasType.Game);
        }
        public void OnClickMainMenu() => NotifyAll(CanvasType.MainMenu);
        public void OnClickShop() => NotifyAll(CanvasType.Shop);
        public void OnClickSettings() => NotifyAll(CanvasType.Settings);
        public void OnClickExit() => Application.Quit();
        private void LoadGameLevel() => SceneManager.LoadSceneAsync("Game");
    }
}