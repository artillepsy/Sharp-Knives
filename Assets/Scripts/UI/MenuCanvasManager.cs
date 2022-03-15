using System.Linq;
using Core;
using Management;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuCanvasManager : AbstractCanvasManager
    {
        private void Start()
        {
            _subs = FindObjectsOfType<MonoBehaviour>().OfType<IOnCanvasChange>().ToList();
            NotifyAll(CanvasType.MainMenu);
            DontDestroyOnLoad(FindObjectOfType<SaveLoadManager>().gameObject);
        }
        public void OnClickStart() => SceneManager.LoadSceneAsync("Game");
        public void OnClickMainMenu() => NotifyAll(CanvasType.MainMenu);
        public void OnClickShop() => NotifyAll(CanvasType.Shop);
    }
}