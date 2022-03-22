using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    /// <summary>
    /// Класс, отвечающий за поведение иконки, демонстрирующий выбранный ножик в окне магазина
    /// </summary>
    public class KnifeShopPreview : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private GameObject unlockParticleSystemPrefab;
        [SerializeField] private GameObject previewParticleSystem;
        [Space]
        [SerializeField] private Image knifeSprite;
        [SerializeField] private GameObject shadowImage;
        [SerializeField] private float changeSpriteDelayInSeconds = 0.3f;
        [SerializeField] private float deactivateShadowDelay = 0.2f;
        [Header("Animation clips")]
        [SerializeField] private AnimationClip appearAnimClip;
        [SerializeField] private AnimationClip disappearAnimClip;
        [SerializeField] private AnimationClip unlockAnimClip;
        private Animation _animation;
        private KnifeShopItem _item;
        /// <summary>
        /// Метод, изменяющий видимость системы частиц на ноже в зависимости от того,
        /// какое окно сейчас видимое
        /// </summary>
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if(newType != CanvasType.Shop) previewParticleSystem.SetActive(false);
            else previewParticleSystem.SetActive(true);
        }
        /// <summary>
        /// Метод, в котором идёт подписка на события показа и открытия ножика,
        /// где вызываются соответствующие анимации
        /// </summary>
        private void OnEnable()
        {
            _animation = GetComponent<Animation>();
            Events.OnClickShowInfo.AddListener((item)=>
            {
                if (_item == item) return;
                _animation.Stop();
                _item = item;
                _animation.Play(disappearAnimClip.name);
                Invoke(nameof(ChangeSprite), changeSpriteDelayInSeconds);
            });
            Events.OnUnlock.AddListener(() =>
            {
                _animation.Stop();
                _animation.Play(unlockAnimClip.name);
                Instantiate(unlockParticleSystemPrefab, transform.position, Quaternion.identity);
                Invoke(nameof(DeactivateShadow), deactivateShadowDelay);
            });
        }
        /// <summary>
        /// Метод, изменяющий спрайт и проигрывающий анимацию появления после изменения
        /// </summary>
        private void ChangeSprite()
        {
            knifeSprite.sprite = _item.KnifeSprite;
            var shouldEnableShadow = !SaveManager.Inst.Shop.UnlockedIds.Contains(_item.Id);
            shadowImage.SetActive(shouldEnableShadow);
            _animation.Play(appearAnimClip.name);
        }
        /// <summary>
        /// Метод, заменяющий мотононный спрайт на обычный
        /// </summary>
        private void DeactivateShadow() => shadowImage.SetActive(false);
        
    }
}