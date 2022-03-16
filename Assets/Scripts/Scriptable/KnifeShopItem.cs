using UnityEngine;

namespace Scriptable
{
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