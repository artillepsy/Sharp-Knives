using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class KnifeShopPreview : MonoBehaviour
    {
        [SerializeField] private Image knifeSprite;
        [SerializeField] private GameObject shadowImage;
        [SerializeField] private float changeSpriteDelayInSeconds = 0.3f;
        [SerializeField] private float deactivateShadowDelay = 0.2f;
        private Animation _animation;
        private readonly string _appearAnimClip = "Appear";
        private readonly string _disappearAnimClip = "Disappear";
        private readonly string _unlockAnimClip = "Unlock";
        private SaveManager _saveManager;
        private KnifeShopItem _item;
        private void OnEnable()
        {
            _animation = GetComponent<Animation>();
            _saveManager = FindObjectOfType<SaveManager>();
            Events.OnClickShowInfo.AddListener((item)=>
            {
                if (_item == item) return;
                _animation.Rewind();
                _item = item;
                _animation.Play(_disappearAnimClip);
                Invoke(nameof(ChangeSprite), changeSpriteDelayInSeconds);
            });
            Events.OnUnlock.AddListener(() =>
            {
                _animation.Rewind();
                _animation.Play(_unlockAnimClip);
                Invoke(nameof(DeactivateShadow), deactivateShadowDelay);
            });
        }

        private void ChangeSprite()
        {
            knifeSprite.sprite = _item.KnifeSprite;
            var shouldEnableShadow = !_saveManager.Shop.UnlockedIds.Contains(_item.Id);
            shadowImage.SetActive(shouldEnableShadow);
            _animation.Play(_appearAnimClip);
        }
        private void DeactivateShadow() => shadowImage.SetActive(false);
    }
}