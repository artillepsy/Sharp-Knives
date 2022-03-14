using UnityEngine;

namespace Core
{
    public class TapInput : MonoBehaviour
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        private float _currentTime = 0;
        private bool _inputEnabled = true;
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        private void OnEnable()
        {
            _currentTime = reloadTimeInSeconds;
            Events.OnKnifeDrop.AddListener(() => _inputEnabled = false);
        }
        private void Update()
        {
            if (!_inputEnabled) return;
            if (!ReadyToThrow()) return;
            if (Input.touchCount == 0) return;
            if (Input.touches[0].phase != TouchPhase.Began) return;
            Throw();
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