using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    /// <summary>
    /// Класс, отвечающий за кликабельную иконку ножа на экране магазина
    /// </summary>
    public class KnifeShopButton : MonoBehaviour
    {
        [Header("Frame")]
        [SerializeField] private Image frame;
        [SerializeField] private Color standartFrameColor;
        [SerializeField] private Color equippedFrameColor;

        [Header("Background")]
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color unlockedBackgroundColor;

        [Header("Sprite")]
        [SerializeField] private Image knifeSprite;
        [SerializeField] private Image knifeWhiteShadow;
        
        [Header("Locked color")]
        [SerializeField] private Color defaultLockedColor = Color.green;
        [SerializeField] private Color bossLockedColor = Color.red;
        private KnifeShopItem _item;
        /// <summary>
        /// Метод посылает ивент, оповещающий о том, что выбран текущий ножик
        /// </summary>
        public void OnClickButton() => Events.OnClickShowInfo?.Invoke(_item);
        /// <summary>
        /// Метод инициализации значений и подписки на события
        /// </summary>
        public void SetValues(KnifeShopItem item)
        {
            _item = item;
            knifeSprite.sprite = _item.KnifeSprite;
            knifeWhiteShadow.color = _item.IsBossDrop ? bossLockedColor : defaultLockedColor;
            CheckUnlockStatus();
            
            var equipped = SaveManager.Inst.Shop.EquippedId == _item.Id;
            frame.gameObject.SetActive(equipped);
            if (equipped)
            {
                frame.color = equippedFrameColor;
                Events.OnClickShowInfo?.Invoke(_item);
            }

            Events.OnUnlock.AddListener(CheckUnlockStatus);
            Events.OnClickShowInfo.AddListener(CheckSelectStatus);
        }
        /// <summary>
        /// Метод открытия ножика. Изменяет визуальные данные иконки. и подписки на события
        /// </summary>
        private void CheckUnlockStatus()
        {
            if (!SaveManager.Inst.Shop.UnlockedIds.Contains(_item.Id)) return;
            Events.OnUnlock.RemoveListener(CheckUnlockStatus);
            Events.OnEquip.AddListener(CheckEquipStatus);
            backgroundImage.color = unlockedBackgroundColor;
            knifeWhiteShadow.enabled = false;
        }
        /// <summary>
        /// Метод, вызывающийся при событии экипировки ножа, проверяет, экипирован ли текущий
        /// нож и изменяет цвет рамки иконки
        /// </summary>
        private void CheckEquipStatus(KnifeShopItem item = null)
        {
            var equipped = SaveManager.Inst.Shop.EquippedId == _item.Id;
            frame.gameObject.SetActive(equipped);
            frame.color = equipped ? equippedFrameColor : standartFrameColor;
        }
        /// <summary>
        /// Метод, вызывающийся при выборе ножика. Проверяет, выбран ли данный ножик, и
        /// изменяет рамку иконки на соответствующий цвет
        /// </summary>
        private void CheckSelectStatus(KnifeShopItem item)
        {
            var equipped = SaveManager.Inst.Shop.EquippedId == _item.Id;
            var selected = _item.Id == item.Id;
            frame.gameObject.SetActive(equipped || selected);
            frame.color = equipped ? equippedFrameColor : standartFrameColor;
        }
    }
}
