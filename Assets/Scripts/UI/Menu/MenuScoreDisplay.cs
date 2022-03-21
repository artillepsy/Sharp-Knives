using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScore;
        private void OnEnable() => Events.OnUnlock.AddListener(ChangeAppleCount);
        private void Start()
        {
            appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
            highScore.text = "High: "+SaveManager.Inst.Score.HighScore.ToString();
        }
        private void ChangeAppleCount() => appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
    }
}