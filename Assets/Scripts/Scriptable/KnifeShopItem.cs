using UnityEngine;

namespace Scriptable
{
    /// <summary>
    /// Класс, хранящий информацию о ноже для окна магазина
    /// </summary>
    [CreateAssetMenu(fileName = "Knife item")]
    public class KnifeShopItem : ScriptableObject
    {
        public int Id;
        public Sprite KnifeSprite;
        public bool IsBossDrop;
        [Header("If not drop")]
        public int Cost = 500;
    }
}