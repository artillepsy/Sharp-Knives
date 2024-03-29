﻿using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UI;
using UI.Shop;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Класс, отвечающий за инициализацию кнопок в окне магазина
    /// </summary>
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private List<KnifeShopItem> knifeItems;
        [SerializeField] private KnifeShopButton knifeShopButtonPrefab;
        private List<KnifeShopButton> _buyButtons;
        public List<KnifeShopItem> KnifeItems => knifeItems;

        /// <summary>
        /// Инициализация кликабельных иконок ножей. Ножи, выдаваемые за
        /// победу над боссами, располагаются в конце
        /// </summary>
        private void Start()
        {
            _buyButtons = new List<KnifeShopButton>();
            List<KnifeShopItem> bossKnifeItems = new List<KnifeShopItem>();
            foreach (var knife in knifeItems)
            {
                if (knife.IsBossDrop)
                {
                    bossKnifeItems.Add(knife);
                    continue;
                }
                var instance = Instantiate(knifeShopButtonPrefab, content);
                instance.SetValues(knife);
                _buyButtons.Add(instance);
            }
            foreach (var knife in bossKnifeItems)
            {
                var instance = Instantiate(knifeShopButtonPrefab, content);
                instance.SetValues(knife);
                _buyButtons.Add(instance);
            }
        }
    }
}