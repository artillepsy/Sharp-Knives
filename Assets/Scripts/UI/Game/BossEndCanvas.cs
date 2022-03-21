using System.Collections.Generic;
using System.Linq;
using Core;
using LevelSettings;
using SaveSystem;
using Scriptable;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BossEndCanvas : MonoBehaviour, IOnLevelLoad, IOnCanvasChange
    {
        [SerializeField] private TextMeshProUGUI unlockedKnifeText;
        [SerializeField] private Image sliderImage;
        [SerializeField] private float delayDisappearAnims = 2f;
        [SerializeField] private float reduceHpTime = 1f;
        [SerializeField] private float delayReduceTime = 1f;
        private List<UIAnimationController> _subs;
        private float _startSliderAmount;
        private float _endSliderAmount;
        private bool _shouldReduce = false;
        
        public void OnLevelLoad(Level level)
        {
            if(!level.Log.Custom.IsBoss) return;
            unlockedKnifeText.enabled = false;
            _subs = GetComponentsInChildren<UIAnimationController>().ToList();
            _startSliderAmount = SaveManager.Inst.Score.BossHP / 100f;
            _endSliderAmount = _startSliderAmount - level.Log.Custom.Boss.DamageAtDestroy / 100f;
            sliderImage.fillAmount = _startSliderAmount;
        }
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if (newType != CanvasType.BossLevelEnd) return;
            Invoke(nameof(StartReduceHP), delayReduceTime);
            Invoke(nameof(NotifySubs), delayDisappearAnims);
        }
        private void OnEnable() => Events.OnUnlock.AddListener(() => unlockedKnifeText.enabled = true);
        private void Update()
        {
            if (_shouldReduce) ReduceHP();
        }

        private void StartReduceHP() => _shouldReduce = true;
        private void ReduceHP()
        {
            if (sliderImage.fillAmount <= _endSliderAmount) _shouldReduce = false;
            sliderImage.fillAmount -= Time.deltaTime * reduceHpTime;
        }
        private void NotifySubs()
        {
            _subs.ForEach(sub => sub.PlayAnimation(false));
        }
    }
}