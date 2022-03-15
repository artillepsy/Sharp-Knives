using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(FindObjectOfType<SaveLoadManager>().gameObject);
        }

        public void OnClickStart()
        {
            SceneManager.LoadSceneAsync("Game");
        }
    }
}