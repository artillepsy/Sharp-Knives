using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private List<KnifeShopItem> knifeItems;
        [SerializeField] private BuyButton buyButtonPrefab;
        private List<BuyButton> _buyButtons;
        private SaveManager _saveManager;
        public List<KnifeShopItem> KnifeItems => knifeItems;

        public void Unlock(KnifeShopItem item)
        {
            _saveManager.Shop.Unlock(item.Id, item.Cost);
            Events.OnBuy?.Invoke(item.Cost);
        }

        public void Equip(KnifeShopItem item)
        {
            _saveManager.Shop.Equip(item);
            _saveManager.Knife.Equip(item);
            Events.OnEquip?.Invoke(item.Id);
        }
        
        private void Start()
        {
            _buyButtons = new List<BuyButton>();
            _saveManager = FindObjectOfType<SaveManager>();
            foreach (var knife in knifeItems)
            {
                var instance = Instantiate(buyButtonPrefab, content);
                instance.SetValues(knife, IsUnlocked(knife),
                    _saveManager.Score.AppleCount,
                    _saveManager.Knife.EquippedKnifeId);
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