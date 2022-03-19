using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class CanvasStateController : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private CanvasType type;
        private List<UIAnimationController> _subs;
        private Canvas _canvas;
        private bool _status = false; 
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _subs = GetComponentsInChildren<UIAnimationController>().ToList();
        }

        public void OnCanvasChange(CanvasType newType, float delay)
        {
            _status = newType == type;
            _subs.ForEach(sub => sub.PlayAnimation(_status));
            Invoke(nameof(ChangeEnableStatus), delay);
        }
        public void ChangeEnableStatus() => _canvas.enabled = _status;
    }
}