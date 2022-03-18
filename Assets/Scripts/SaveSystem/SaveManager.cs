﻿using System.Collections.Generic;
using Core;
using Management;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private UserData _userData;
        public KnifeData Knife;
        public ShopData Shop;
        public SoundData Sound;
        public ScoreData Score;
        public void Save() => SaveSystem.Save(_userData);
        private void OnEnable()
        {
            Events.OnAppleHit.AddListener( () =>Score.IncrementApples());
            Events.OnUnlock.AddListener(Shop.Unlock);
            Events.OnEquip.AddListener(Shop.Equip);
            Events.OnEquip.AddListener(Knife.Equip);
            Events.OnDefeatBoss.AddListener(Shop.UnlockBossKnife);
        }
        private void Awake()
        {
            Test_ClearProgress();
            _userData = SaveSystem.Load();
            if (_userData == null)
            {
                _userData = new UserData(0, 1, new List<int>(){1});
            }
            Knife = new KnifeData(_userData);
            Shop = new ShopData(_userData);
            Sound = new SoundData(_userData);
            Score = new ScoreData(_userData);
        }
        private void Start()
        {
            var shopManager = FindObjectOfType<ShopManager>(true);
            Shop.Knives = shopManager.KnifeItems;
            foreach (var knifeItem in shopManager.KnifeItems)
            {
                if (knifeItem.Id != _userData.EquippedKnifeId) continue;
                Knife.CurrentSprite = knifeItem.KnifeSprite;
            }
        }
        private void Test_ClearProgress()
        {
            _userData = new UserData(100, 1, new List<int>(){1});
            SaveSystem.Save(_userData);
        }
    }
}