using SaveSystem;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Класс, инициализирующий параметры при старте
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            SaveManager.Inst.Score.CurrentScore = 0;
        }
    }
}