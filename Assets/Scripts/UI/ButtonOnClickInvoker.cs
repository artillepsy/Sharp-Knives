using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Класс для всех кнопок. Вызывает событие при нажатии 
    /// </summary>
    public class ButtonOnClickInvoker : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=>Events.OnClickButton?.Invoke());
        }
    }
}