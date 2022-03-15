using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScore;

        private void Start()
        {
            var manager = FindObjectOfType<SaveLoadManager>();
            appleCount.text = manager.AppleCount.ToString();
            highScore.text = manager.HighScore.ToString();
        }
    }
}