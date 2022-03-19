using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScore;
        private SaveManager _saveManager;
        private void OnEnable() => Events.OnUnlock.AddListener(ChangeAppleCount);
        private void Start()
        {
            _saveManager = FindObjectOfType<SaveManager>();
            appleCount.text = _saveManager.Score.AppleCount.ToString();
            highScore.text = "High: "+_saveManager.Score.HighScore.ToString();
        }
        private void ChangeAppleCount() => appleCount.text = _saveManager.Score.AppleCount.ToString();
    }
}