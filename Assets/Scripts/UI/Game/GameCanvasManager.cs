using System.Linq;
using Core;
using Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Game
{
    /// <summary>
    /// Класс, регулирующий отображение окон во время игры
    /// </summary>
    public class GameCanvasManager : AbstractCanvasManager, IOnLevelLoad
    {
        [SerializeField] private float bossStartCanvasShowTime = 3f;
        
        private bool _isBoss = false;
        /// <summary>
        /// Метод проверяет, является ли уровень битвой с боссом. Если да,
        /// то вызывается стартовое окно битвы с боссом и открадывается его
        /// исчезновение
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            _isBoss = level.Log.Settings.IsBoss;

            if (_isBoss)
            {
                NotifyAll(CanvasType.BossLevelStart, false);
                Invoke(nameof(ShowGameCanvas), bossStartCanvasShowTime);
            }
            else NotifyAll(CanvasType.Game, false);
        }
        public void OnClickPause() => NotifyAll(CanvasType.Pause);
        public void OnClickRestart() =>  SceneManager.LoadSceneAsync("Game");
        public void OnClickResume() => NotifyAll(CanvasType.Game); 
        public void OnClickMainMenu() => Invoke(nameof(LoadMenuLevel), changeDelayInSeconds);
        private void OnEnable()
        {
            _subs = FindObjectsOfType<MonoBehaviour>().OfType<IOnCanvasChange>().ToList();
            Events.OnFailGame.AddListener(() => NotifyAll(CanvasType.Fail));
            Events.OnWinGame.AddListener(() =>
            {
                if (_isBoss) NotifyAll(CanvasType.BossLevelEnd);
            });
        }
        /// <summary>
        /// Метод, возвращающий окно уровня. Вызывается с задержкой
        /// </summary>
        private void ShowGameCanvas() => NotifyAll(CanvasType.Game);
        private void LoadMenuLevel() => SceneManager.LoadSceneAsync("Menu");
        
    }
}