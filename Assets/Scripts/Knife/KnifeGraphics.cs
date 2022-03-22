using SaveSystem;
using UnityEngine;

namespace Knife
{
    /// <summary>
    /// Класс, отвечающий за инициализацию спрайта ножав начале уровня
    /// тем спрайтом, который был выбран в магазине в меню
    /// </summary>
    public class KnifeGraphics : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SaveManager>().Knife.CurrentSprite;
        }
    }
}