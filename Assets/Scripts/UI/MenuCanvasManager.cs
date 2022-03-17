using System.Linq;
using Management;
using SaveSystem;
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
            DontDestroyOnLoad(FindObjectOfType<SaveManager>().gameObject); // also audioManager
        }
        public void OnClickStart() => SceneManager.LoadSceneAsync("Game");
        public void OnClickMainMenu() => NotifyAll(CanvasType.MainMenu);
        public void OnClickShop() => NotifyAll(CanvasType.Shop);
    }
}