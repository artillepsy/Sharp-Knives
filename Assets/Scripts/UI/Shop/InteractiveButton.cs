using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    /// <summary>
    /// Класс, отвечающий за поведение кнопки взаимодействия в магазине
    /// </summary>
    public class InteractiveButton : MonoBehaviour
    {
        [SerializeField] private GameObject priceGO;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private GameObject bossDropGO;
        [SerializeField] private GameObject unlockedGO;
        [SerializeField] private GameObject equippedGO;
        private List<GameObject> _buttonGOs;
        private KnifeShopItem _selectedItem;
        private Button _button;

        /// <summary>
        /// Инициализация дочерних UI элементов кнопки и установка их в неактивный статус
        /// </summary>
        private void OnEnable()
        {
            _buttonGOs = new List<GameObject>() {priceGO, bossDropGO, unlockedGO, equippedGO};
            _buttonGOs.ForEach(go => go.SetActive(false));
            _button = GetComponent<Button>();
            _button.enabled = false;
            Events.OnClickShowInfo.AddListener(SelectKnife);
        }
        /// <summary>
        /// Метод добавляет идентификатор ножика в список открытых и меняет
        /// UI данной кнопки
        /// </summary>
        private void OnClickUnlock()
        {
            SaveManager.Inst.Shop.Unlock(_selectedItem);
            SelectKnife(_selectedItem);
        }
        /// <summary>
        /// Метод меняет идентификатор экипированного ножа на выбранный и меняет UI кнопки
        /// </summary>
        private void OnClickEquip()
        {
            SaveManager.Inst.Shop.Equip(_selectedItem);
            SelectKnife(_selectedItem);
        }
        /// <summary>
        /// Метод, отвечающий за отображение дочерних UI элементов и за активность самой кнопки
        /// </summary>
        private void SelectKnife(KnifeShopItem item)
        {
            _selectedItem = item;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> Events.OnClickButton?.Invoke());
            if (SaveManager.Inst.Shop.UnlockedIds.Contains(item.Id))
            {
                var equipped = SaveManager.Inst.Shop.EquippedId == item.Id;
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
            _button.enabled = item.Cost <= SaveManager.Inst.Score.AppleCount;
            _button.onClick.AddListener(OnClickUnlock);
        }
        /// <summary>
        /// Метод, устанавливающий выбранный дочерний UI элемент в активный статус, делая остальные
        /// неактивными
        /// </summary>
        private void SetActiveGO(GameObject obj) => _buttonGOs.ForEach(go => go.SetActive(obj == go));
    }
}