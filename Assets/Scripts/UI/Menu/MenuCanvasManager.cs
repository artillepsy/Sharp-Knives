using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class MenuCanvasManager : AbstractCanvasManager
    {
        private void Start()
        {
            _subs = FindObjectsOfType<MonoBehaviour>().OfType<IOnCanvasChange>().ToList();
            NotifyAll(CanvasType.MainMenu, false);
        }
        public void OnClickStart()
        {
            Invoke(nameof(LoadGameLevel), changeDelayInSeconds);
            NotifyAll(CanvasType.Game);
        }
        public void OnClickMainMenu() => NotifyAll(CanvasType.MainMenu);
        public void OnClickShop() => NotifyAll(CanvasType.Shop);
        private void LoadGameLevel() => SceneManager.LoadSceneAsync("Game");
    }
}