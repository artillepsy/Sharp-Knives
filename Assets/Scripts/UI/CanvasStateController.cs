using UnityEngine;

namespace UI
{
    public class CanvasStateController : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private CanvasType type;
        private Canvas _canvas;
        private void Awake() => _canvas = GetComponent<Canvas>();
        public void OnCanvasChange(CanvasType newType) => _canvas.enabled = newType == type;
    }
}