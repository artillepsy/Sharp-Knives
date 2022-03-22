using System;
using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    /// <summary>
    /// Класс, отвечающий за условия победы и поражения во время игры
    /// </summary>
    public class GameManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private string levelName = "Game";
        [SerializeField] private float newSceneLoadTime = 3f;
        [SerializeField] private float afterBossWinLoadTime = 6f;
        private int _knifeCount;
        private Level _level;
        private bool _bossLevel = false;
        public void SetKnifeCount(int count) => _knifeCount = count;
        public void OnLevelLoad(Level level)
        {
            _level = level;
            _bossLevel = level.Log.Settings.IsBoss;
        }
        public void LoadNewLevel() => SceneManager.LoadSceneAsync(levelName);
        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
            Events.OnKnifeHit.AddListener(OnKnifeHit);
            Events.OnAppleHit.AddListener(IncrementApples);
        }
        private void OnDisable()
        {
            Events.OnKnifeDrop.RemoveListener(OnKnifeDrop);
            Events.OnKnifeHit.RemoveListener(OnKnifeHit);
            Events.OnAppleHit.RemoveListener(IncrementApples);
        }

        private void IncrementApples() => SaveManager.Inst.Score.IncrementApples();

        /// <summary>
        /// метод, срабатывающий при втыкании ножа в бревно и инкрементирующий
        /// значение очков. Проверяет основное условие для победы - равно ли количество
        /// оставшихся ножей нулю
        /// </summary>
        private void OnKnifeHit()
        {
            _knifeCount--;
            SaveManager.Inst.Score.CurrentScore++;
            if(_knifeCount == 0) OnWin();
        }
        /// <summary>
        /// Метод, вызываемый при поражении. Он сбрасывает сохраняеые значения и
        /// устанавливает новый рекорд, если старый был побит
        /// </summary>
        private void OnKnifeDrop()
        {
           Invoke(nameof(LoadGameOverScreen), newSceneLoadTime);
           if (SaveManager.Inst.Score.HighScore < SaveManager.Inst.Score.CurrentScore)
           {
               SaveManager.Inst.Score.HighScore = SaveManager.Inst.Score.CurrentScore;
           }
           SaveManager.Inst.Score.ResetWinCount();
           SaveManager.Inst.Score.CurrentScore = 0;
           SaveManager.Inst.Save();
        }
        /// <summary>
        /// Метод, вызываемый при победе. Он инкрементирует счетчик побед и вызывает событие победы
        /// </summary>
        private void OnWin()
        {
            var delay = _bossLevel ? afterBossWinLoadTime : newSceneLoadTime;
            Invoke(nameof(LoadNewLevel), delay);
            if (_bossLevel)
            {
                SaveManager.Inst.Score.DamageBoss(_level.Log.Settings.Boss.DamageAtDestroy);
            }
            SaveManager.Inst.Score.IncrementWins();
            SaveManager.Inst.Save();
            Events.OnWinGame?.Invoke();
        }
        /// <summary>
        /// Метод, вызываемый с задержкой, оповещающий о поражении
        /// </summary>
        private void LoadGameOverScreen() => Events.OnFailGame?.Invoke();
        
    }
}