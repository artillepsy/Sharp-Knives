using System.Collections.Generic;
using System.Linq;
using Core;
using SaveSystem;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    /// <summary>
    /// Класс, регулирующий окно успешного окончания битвы с боссом
    /// </summary>
    public class BossEndCanvas : MonoBehaviour, IOnLevelLoad, IOnCanvasChange
    {
        [SerializeField] private TextMeshProUGUI unlockedKnifeText;
        [SerializeField] private TextMeshProUGUI bossDefeatText;
        [SerializeField] private Image sliderImage;
        [SerializeField] private float delayDisappearAnims = 2f;
        [SerializeField] private float reduceHpTime = 1f;
        [SerializeField] private float delayReduceTime = 1f;
        private List<UIAnimationController> _subs;
        private float _startSliderAmount;
        private float _endSliderAmount;
        private bool _shouldReduce = false;
        /// <summary>
        /// Настройка отображения очков здоровья, текста открытия ножей и победы над боссом
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            if(!level.Log.Settings.IsBoss) return;
            unlockedKnifeText.enabled = false;
            bossDefeatText.enabled = false;
            _subs = GetComponentsInChildren<UIAnimationController>().ToList();
            _startSliderAmount = SaveManager.Inst.Score.BossHP / 100f;
            _endSliderAmount = _startSliderAmount - level.Log.Settings.Boss.DamageAtDestroy / 100f;
            sliderImage.fillAmount = _startSliderAmount;
        }
        /// <summary>
        /// Отсрочивание некоторых действий для красивой анимации
        /// </summary>
        /// <param name="newType"></param>
        /// <param name="timeInSeconds"></param>
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if (newType != CanvasType.BossLevelEnd) return;
            Invoke(nameof(StartReduceHP), delayReduceTime);
            Invoke(nameof(NotifySubs), delayDisappearAnims);
        }
        /// <summary>
        /// Подписка на события, при срабатывании которых появляется соответствующий текст в окне 
        /// </summary>
        private void OnEnable()
        {
            Events.OnUnlock.AddListener(() => unlockedKnifeText.enabled = true);
            Events.OnDefeatBoss.AddListener(() => bossDefeatText.enabled = true);
            
        }
        /// <summary>
        /// проверка на то, нужно ли уменьшать здоровье босса
        /// </summary>
        private void Update()
        {
            if (_shouldReduce) ReduceHP();
        }
        
        private void StartReduceHP() => _shouldReduce = true;
        /// <summary>
        /// Изменение отображения здоровья босса
        /// </summary>
        private void ReduceHP()
        {
            if (sliderImage.fillAmount <= _endSliderAmount) _shouldReduce = false;
            sliderImage.fillAmount -= Time.deltaTime * reduceHpTime;
        }
        /// <summary>
        /// Оповещение о изменении канваса для подписчиков
        /// </summary>
        private void NotifySubs()
        {
            _subs.ForEach(sub => sub.PlayAnimation(false));
        }
    }
}