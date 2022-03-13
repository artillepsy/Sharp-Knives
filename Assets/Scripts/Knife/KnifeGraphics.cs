using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Knife
{
    public class KnifeGraphics : MonoBehaviour, IOnLevelLoad
    {
        private Sprite _sprite;
        
        public void OnLevelLoad(Level level)
        {
            _sprite = level.KnifeSprite;
        }

        private void Awake()
        {
            _sprite = GetComponent<SpriteRenderer>().sprite;
        }
    }
}