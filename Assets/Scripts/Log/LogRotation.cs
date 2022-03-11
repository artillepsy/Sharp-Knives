using UnityEngine;
using Random = UnityEngine.Random;

namespace Log
{
    public class LogRotation : MonoBehaviour
    {
        [Header("Rotation")] 
        [SerializeField] private Vector3 rotationUp = Vector3.right;
        [SerializeField] private float minRotationSpeed = 3f;
        [SerializeField] private float maxRotationSpeed = 6f;
        [Space] 
        [SerializeField] private float minRotationTime = 5f;
        [SerializeField] private float maxRotationTime = 8f;
        [Space] 
        [SerializeField] private bool alwaysSwapDirection = true;
        [Space] 
        [SerializeField] private AnimationCurve speedChangeAnimationCurve;

        private int _direction;
        private float _currentTime;
        private float _currentRotationSpeed;
        private float _currentRotationTime;
        private float _currentMaxRotationSpeed;
        private float RandomRotationTime => Random.Range(minRotationTime, maxRotationTime);
        private float RandomRotationSpeed => Random.Range(minRotationSpeed, maxRotationSpeed);

        private void Start()
        {
            _direction = Random.value > 0.5f ? 1 : -1;
            StartNewRotation();
        }

        private void Update()
        {
            UpdateRotationSpeed();
            if (ReadyToStartNewRotation()) StartNewRotation();
        }

        private void StartNewRotation()
        {
            _currentMaxRotationSpeed = RandomRotationSpeed;
            _currentRotationTime = RandomRotationTime;
            _currentTime = _currentRotationTime;
            if (alwaysSwapDirection) _direction = -_direction;
            else _direction = Random.value > 0.5f ? 1 : -1;
            _currentMaxRotationSpeed *= _direction;
        }

        private void UpdateRotationSpeed()
        {
            _currentRotationSpeed = speedChangeAnimationCurve.Evaluate(_currentTime / _currentRotationTime) 
                                    * _currentMaxRotationSpeed;
            transform.Rotate(rotationUp, _currentRotationSpeed*Time.deltaTime);
        }
        
        private bool ReadyToStartNewRotation()
        {
            if (_currentTime <= 0) return true;
            _currentTime -= Time.deltaTime;
            return false;
        }
    }
}
