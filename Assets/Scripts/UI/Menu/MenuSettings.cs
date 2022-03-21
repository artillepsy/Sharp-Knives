using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MenuSettings : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle vibrationToggle;
        public void OnClickSave() => SaveManager.Inst.Sound.SetVolumeSettings(volumeSlider.value, vibrationToggle.isOn);
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if (newType != CanvasType.Settings) return;
            volumeSlider.value = SaveManager.Inst.Sound.Volume;
            vibrationToggle.isOn = SaveManager.Inst.Sound.Vibration;
        }
    }
}