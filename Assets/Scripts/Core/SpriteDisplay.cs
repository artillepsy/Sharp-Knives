using UnityEngine;

namespace Core
{
    public class SpriteDisplay : MonoBehaviour
    {
        [SerializeField] private SpriteData spriteData;
        
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = spriteData.sprite;
        }
    }
}