using UnityEngine;

namespace Management
{
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
        }
    }
}