using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleScore;
        [SerializeField] private TextMeshProUGUI hitScore;
        
        private int _appleCount = 0;
        private int _hitScore = 0;

        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(IncrementApples);
            Events.OnKnifeHit.AddListener(IncrementScore);
        }

        private void Start()
        {
            var manager = FindObjectOfType<SaveLoadManager>();
            _appleCount = manager.AppleCount;
            appleScore.text = _appleCount.ToString();
            _hitScore = manager.CurrentScore;
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
    }
}