using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonAudioClick : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=>Events.OnClikButton?.Invoke());
        }
    }
}