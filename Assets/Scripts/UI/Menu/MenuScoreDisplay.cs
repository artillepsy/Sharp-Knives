using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI stageText;
        
        private void OnEnable() => Events.OnUnlock.AddListener(ChangeAppleCount);
        private void Start()
        {
            appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
            highScoreText.text = "High: " + SaveManager.Inst.Score.HighScore;
            stageText.text = "Stage "+ (SaveManager.Inst.Score.WinCount+1);
        }
        private void ChangeAppleCount() => appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
    }
}