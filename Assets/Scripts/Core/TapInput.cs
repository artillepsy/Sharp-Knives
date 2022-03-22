using UI;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Класс, отвечающий за отслеживание нажатий во время игры
    /// </summary>
    public class TapInput : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        private Collider _tapZoneCollider;
        private float _currentTime = 0;
        private bool _inputEnabled = true;
        private Camera _camera;
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        /// <summary>
        /// Если игровой канвас скрыт, то отслеживание нажатий отключено
        /// </summary>
        public void OnCanvasChange(CanvasType newType, float time)
        {
            _inputEnabled = newType == CanvasType.Game;
        }
        /// <summary>
        /// Данный метод включает в себя подписку на игровые моменты, после которых отслеживание нажатий прекращается
        /// </summary>
        private void OnEnable()
        {
            _tapZoneCollider = GetComponentInChildren<Collider>();
            _camera = Camera.main;
            _currentTime = reloadTimeInSeconds;
            Events.OnKnifeDrop.AddListener(() => _inputEnabled = false);
            Events.OnWinGame.AddListener(() => _inputEnabled = false);
        }
        /// <summary>
        /// Отслеживание нажатий. Если все условия соблюдены, то вызывается метод бросания ножа
        /// </summary>
        private void Update()
        {
            if (!_inputEnabled) return;
            if (!ReadyToThrow()) return;
            if (Input.touchCount == 0) return;
            if (Input.touches[0].phase != TouchPhase.Began) return;
            var ray = _camera.ScreenPointToRay(Input.touches[0].position);
            if (!Physics.Raycast(ray, out var hit)) return;
            if(hit.collider  == _tapZoneCollider) Throw();            
        }
        /// <summary>
        /// Срабатывание ивента бросания ножа и обновление таймера задержки нового отслеживания
        /// </summary>
        private void Throw()
        {
            Events.OnThrow?.Invoke();
            _currentTime = reloadTimeInSeconds;
        }
        /// <summary>
        /// проверка то, сколько прошло времени с момента предыдущего броска, и возвращение результата о готовности нового
        /// </summary>
        private bool ReadyToThrow()
        {
            if (_currentTime <= 0) return true;
            _currentTime -= Time.deltaTime;
            return false;
        }
    }
}