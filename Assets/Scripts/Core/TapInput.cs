using Management;
using UI;
using UnityEngine;

namespace Core
{
    public class TapInput : MonoBehaviour, IOnCanvasChange
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        private Collider _tapZoneCollider;
        private float _currentTime = 0;
        private bool _inputEnabled = true;
        private Camera _camera;
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        public void OnCanvasChange(CanvasType newType, float time)
        {
            _inputEnabled = newType == CanvasType.Game;
        }
        private void OnEnable()
        {
            _tapZoneCollider = GetComponentInChildren<Collider>();
            _camera = Camera.main;
            _currentTime = reloadTimeInSeconds;
            Events.OnKnifeDrop.AddListener(() => _inputEnabled = false);
            Events.OnWinGame.AddListener(() => _inputEnabled = false);
        }
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
        private void Throw()
        {
            Events.OnThrow?.Invoke();
            _currentTime = reloadTimeInSeconds;
        }
        private bool ReadyToThrow()
        {
            if (_currentTime <= 0) return true;
            _currentTime -= Time.deltaTime;
            return false;
        }
    }
}