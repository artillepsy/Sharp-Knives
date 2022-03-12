using UnityEngine;

namespace Core
{
    public class TapInput : MonoBehaviour
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        private float _currentTime = 0;
        private bool _inputEnabled = true;

        private void Awake()
        {
            _currentTime = reloadTimeInSeconds;
        }

        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(() => _inputEnabled = false);
        }

        private void Update()
        {
            if (!_inputEnabled) return;
            if (!ReadyToThrow()) return;
            if (Input.touchCount > 0) Throw();
        }

        private void Throw()
        {
            Events.OnTap?.Invoke();
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