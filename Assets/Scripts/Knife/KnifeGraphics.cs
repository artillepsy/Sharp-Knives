using Core;
using UnityEngine;

namespace Knife
{
    public class KnifeGraphics : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SaveLoadManager>().CurrentKnifeSprite;
        }
    }
}