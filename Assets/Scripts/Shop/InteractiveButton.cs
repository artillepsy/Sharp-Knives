using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class InteractiveButton : MonoBehaviour
    {
        [SerializeField] private GameObject priceGO;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private GameObject bossDropGO;
        [SerializeField] private GameObject unlockedGO;
        [SerializeField] private GameObject equippedGO;
        private List<GameObject> _buttonGOs;
        private KnifeShopItem _selectedItem;
        private SaveManager _saveManager;
        private Button _button;

        private void OnEnable()
        {
            _buttonGOs = new List<GameObject>() {priceGO, bossDropGO, unlockedGO, equippedGO};
            _buttonGOs.ForEach(go => go.SetActive(false));
            _button = GetComponent<Button>();
            _button.enabled = false;
            _saveManager = FindObjectOfType<SaveManager>();
            Events.OnClickShowInfo.AddListener(SelectKnife);
        }

        private void OnClickUnlock()
        {
            _saveManager.Shop.Unlock(_selectedItem);
            SelectKnife(_selectedItem);
        }

        private void OnClickEquip()
        {
            _saveManager.Shop.Equip(_selectedItem);
            SelectKnife(_selectedItem);
        }

        private void SelectKnife(KnifeShopItem item)
        {
            _selectedItem = item;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> Events.OnClikButton?.Invoke());
            if (_saveManager.Shop.UnlockedIds.Contains(item.Id))
            {
                var equipped = _saveManager.Shop.EquippedId == item.Id;
                SetActiveGO(equipped ? equippedGO : unlockedGO);
                _button.enabled = !equipped;
                if (!equipped) _button.onClick.AddListener(OnClickEquip);
                return;
            }

            if (item.IsBossDrop)
            {
                _button.enabled = false;
                SetActiveGO(bossDropGO);
                return;
            }

            SetActiveGO(priceGO);
            priceText.text = item.Cost.ToString();
            _button.enabled = item.Cost <= _saveManager.Score.AppleCount;
            _button.onClick.AddListener(OnClickUnlock);
        }
        private void SetActiveGO(GameObject obj) => _buttonGOs.ForEach(go => go.SetActive(obj == go));
    }
}