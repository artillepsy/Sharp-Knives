using SaveSystem;
using UnityEngine;

namespace Knife
{
    public class KnifeGraphics : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SaveManager>().CurrentKnifeSprite;
        }
    }
}