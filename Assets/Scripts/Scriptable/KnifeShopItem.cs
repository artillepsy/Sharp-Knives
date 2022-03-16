using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Knife item")]
    public class KnifeShopItem : ScriptableObject
    {
        public int Id;
        public Sprite KnifeSprite;
        public bool IsBossDrop;
        public int Cost = 500;
    }
}