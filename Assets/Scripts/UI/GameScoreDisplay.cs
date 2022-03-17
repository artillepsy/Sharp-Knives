using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleScore;
        [SerializeField] private TextMeshProUGUI hitScore;
        [Space] 
        [SerializeField] private string currentScoreText = "Score:";
        [SerializeField] private string highScoreText = "High:";
        [SerializeField] private TextMeshProUGUI failCanvasCurrentScore;
        [SerializeField] private TextMeshProUGUI failCanvasHighScore;

        private SaveManager _manager;
        private int _appleCount = 0;
        private int _hitScore = 0;

        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(IncrementApples);
            Events.OnKnifeHit.AddListener(IncrementScore);
            Events.OnKnifeDrop.AddListener(DisplayFailScore);
        }

        private void Start()
        {
            _manager = FindObjectOfType<SaveManager>();
            _appleCount = _manager.Score.AppleCount;
            appleScore.text = _appleCount.ToString();
            _hitScore = _manager.Score.CurrentScore;
            hitScore.text = _hitScore.ToString();
        }

        private void IncrementApples()
        {
            _appleCount++;
            appleScore.text = _appleCount.ToString();
        }

        private void IncrementScore()
        {
            _hitScore++;
            hitScore.text = _hitScore.ToString();
        }

        private void DisplayFailScore()
        {
            failCanvasCurrentScore.text = currentScoreText + " " + _hitScore;
            failCanvasHighScore.text = highScoreText + " " + _manager.Score.HighScore;
        }
    }
}