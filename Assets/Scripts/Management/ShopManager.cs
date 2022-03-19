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
        [SerializeField] private KnifeShopButton knifeShopButtonPrefab;
        private List<KnifeShopButton> _buyButtons;
        private SaveManager _saveManager;
        public List<KnifeShopItem> KnifeItems => knifeItems;

        private void Start()
        {
            _buyButtons = new List<KnifeShopButton>();
            _saveManager = FindObjectOfType<SaveManager>();
            foreach (var knife in knifeItems)
            {
                var instance = Instantiate(knifeShopButtonPrefab, content);
                instance.SetValues(knife);
                _buyButtons.Add(instance);
            }
            // add items player bought
        }
    }
}