using Level;
using UnityEngine;

namespace Knife
{
    public class KnifeGraphics : MonoBehaviour, IOnLevelLoad
    {
        private Sprite _sprite;
        
        public void OnLevelLoad(LevelData levelData)
        {
            _sprite = levelData.KnifeSprite;
        }

        private void Awake()
        {
            _sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }
}