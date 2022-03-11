using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class TouchInput : MonoBehaviour
    {
        [SerializeField] private float reloadTimeInSeconds = 1f;
        public float ReloadTimeInSeconds => reloadTimeInSeconds;
        private float _currentTime = 0;
        public static UnityEvent OnTouch = new UnityEvent();
        
        private void Update()
        {
            if (!ReadyToThrow()) return;
            if (Input.touchCount > 0) Throw();
        }

        private void Throw()
        {
            OnTouch?.Invoke();
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