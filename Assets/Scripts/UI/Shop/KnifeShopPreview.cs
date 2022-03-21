﻿using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
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
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0)
        {
            if(newType != CanvasType.Shop) previewParticleSystem.SetActive(false);
            else previewParticleSystem.SetActive(true);
        }
        private void OnEnable()
        {
            _animation = GetComponent<Animation>();
            Events.OnClickShowInfo.AddListener((item)=>
            {
                if (_item == item) return;
                _animation.Rewind();
                _item = item;
                _animation.Play(disappearAnimClip.name);
                Invoke(nameof(ChangeSprite), changeSpriteDelayInSeconds);
            });
            Events.OnUnlock.AddListener(() =>
            {
                _animation.Rewind();
                _animation.Play(unlockAnimClip.name);
                Instantiate(unlockParticleSystemPrefab, transform.position, Quaternion.identity);
                Invoke(nameof(DeactivateShadow), deactivateShadowDelay);
            });
        }

        private void ChangeSprite()
        {
            knifeSprite.sprite = _item.KnifeSprite;
            var shouldEnableShadow = !SaveManager.Inst.Shop.UnlockedIds.Contains(_item.Id);
            shadowImage.SetActive(shouldEnableShadow);
            _animation.Play(appearAnimClip.name);
        }
        private void DeactivateShadow() => shadowImage.SetActive(false);
        
    }
}