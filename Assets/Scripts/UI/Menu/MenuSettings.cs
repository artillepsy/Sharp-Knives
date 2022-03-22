using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    /// <summary>
    /// Класс, отвечающий за окно настроек
    /// </summary>
    public class MenuSettings : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle vibrationToggle;
        /// <summary>
        /// Метод сохраняет измененные значения в файл сохранения
        /// </summary>
        public void OnClickSave() => SaveManager.Inst.Sound.SetVolumeSettings(volumeSlider.value, vibrationToggle.isOn);
        /// <summary>
        /// При появлении окна настроек, значения берутся из файла
        /// </summary>
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if (newType != CanvasType.Settings) return;
            volumeSlider.value = SaveManager.Inst.Sound.Volume;
            vibrationToggle.isOn = SaveManager.Inst.Sound.Vibration;
        }
    }
}