using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    /// <summary>
    /// Класс отображения счёта и яблок в главном меню
    /// </summary>
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI stageText;
        /// <summary>
        /// Подписка на событие при открытии ножа, после которого изменяется количество яблок
        /// </summary>
        private void OnEnable() => Events.OnUnlock.AddListener(ChangeAppleCount);
        /// <summary>
        /// Инициализация текстовых элементов значениями из файла сохранения
        /// </summary>
        private void Start()
        {
            appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
            highScoreText.text = "High: " + SaveManager.Inst.Score.HighScore;
            stageText.text = "Stage "+ (SaveManager.Inst.Score.WinCount+1);
        }
        /// <summary>
        /// Изменение количества яблок, которое берется из файла сохранения
        /// </summary>
        private void ChangeAppleCount() => appleCount.text = SaveManager.Inst.Score.AppleCount.ToString();
    }
}