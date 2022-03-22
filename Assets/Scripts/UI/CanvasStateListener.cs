using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Класс, реагирующий на изменение состояний окон
    /// </summary>
    public class CanvasStateListener : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private CanvasType type;
        private List<UIAnimationController> _subs;
        private Canvas _canvas;
        private bool _status = false; 
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _subs = GetComponentsInChildren<UIAnimationController>(true).ToList();
            _subs.AddRange(GetComponents<UIAnimationController>().ToList());
        }

        /// <summary>
        /// Метод изменяет видимость окна и проигрывает анимации у подписчиков в зависимости от того,
        /// равен ли нынешний тип значению из инспектора
        /// </summary>
        public void OnCanvasChange(CanvasType newType, float delay)
        {
            _status = newType == type;
            if (!_status) _subs.ForEach(sub => sub.PlayAnimation(_status));
            Invoke(nameof(ChangeEnableStatus), delay);
        }
        /// <summary>
        /// Метод изменяет видимость окна и проигрывает соответствующую анимацию у подписчиков.
        /// Вызывается с задержкой
        /// </summary>
        public void ChangeEnableStatus()
        {
            _canvas.enabled = _status;
            if (_status) _subs.ForEach(sub => sub.PlayAnimation(_status));
        }
    }
}