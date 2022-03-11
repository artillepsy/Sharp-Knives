using Knife;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class LevelManager : MonoBehaviour
    {
        private void OnEnable()
        {
            KnifeStateController.OnDrop?.AddListener(EndLevel);
        }

        private void EndLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}