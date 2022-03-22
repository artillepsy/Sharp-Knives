using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI.Game
{
    /// <summary>
    /// КЛасс, отвечающий за отображение счета во время игры
    /// </summary>
    public class GameScoreDisplay : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private string currentScoreText = "Score:";
        [SerializeField] private string highScoreText = "High:";
        [Header("Game Canvas")]
        [SerializeField] private TextMeshProUGUI appleScore;
        [SerializeField] private TextMeshProUGUI hitScore;
        [Header("Fail Canvas")]
        [SerializeField] private TextMeshProUGUI failCanvasCurrentScore;
        [SerializeField] private TextMeshProUGUI failCanvasHighScore;
        [Header("Pause Canvas")]
        [SerializeField] private TextMeshProUGUI pauseCanvasCurrentScore;
        [SerializeField] private TextMeshProUGUI pauseCanvasHighScore;

        private SaveManager _manager;
        private int _appleCount = 0;
        private int _hitScore = 0;

        /// <summary>
        /// При изменении окна обновляются соответствующие UI элементы
        /// </summary>
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            switch (newType)
            {
                case CanvasType.Fail:
                    UpdateFailScore();
                    break;
                case CanvasType.Pause:
                    UpdatePauseScore();
                    break;
            }
        }
        
        private void OnEnable()
        {
            Events.OnAppleHit.AddListener(IncrementApples);
            Events.OnKnifeHit.AddListener(IncrementScore);
            Events.OnKnifeDrop.AddListener(UpdateFailScore);
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
            _appleCount+=2;
            appleScore.text = _appleCount.ToString();
        }

        private void IncrementScore()
        {
            _hitScore++;
            hitScore.text = _hitScore.ToString();
        }
        /// <summary>
        /// Обновление счета на экране поражения
        /// </summary>
        private void UpdateFailScore()
        {
            failCanvasCurrentScore.text = currentScoreText + " " + _hitScore;
            failCanvasHighScore.text = highScoreText + " " + _manager.Score.HighScore;
        }
        /// <summary>
        /// Обновление счета на экране паузы
        /// </summary>
        private void UpdatePauseScore()
        {
            pauseCanvasCurrentScore.text = currentScoreText + " " + _hitScore;
            pauseCanvasHighScore.text = highScoreText + " " + _manager.Score.HighScore;
        }
        
    }
}