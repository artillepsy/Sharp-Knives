using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UI;
using UnityEngine;

namespace Management
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private List<KnifeShopItem> knifeItems;
        [SerializeField] private BuyButton buyButtonPrefab;
        private List<BuyButton> _buyButtons;
        private SaveManager _saveManager;
        public List<KnifeShopItem> KnifeItems => knifeItems;

        private void Start()
        {
            _buyButtons = new List<BuyButton>();
            _saveManager = FindObjectOfType<SaveManager>();
            foreach (var knife in knifeItems)
            {
                var instance = Instantiate(buyButtonPrefab, content);
                instance.SetValues(knife);
                _buyButtons.Add(instance);
            }
            // add items player bought
        }
        private bool IsUnlocked(KnifeShopItem item)
        {
            foreach (var id in _saveManager.Shop.UnlockedIds)
            {
                if (item.Id == id) return true;
            }
            return false;
        }
    }
}