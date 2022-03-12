using Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Log
{
    public class LogRotation : MonoBehaviour, IOnLevelLoad
    {
        [Header("Rotation")] 
        [SerializeField] private Vector3 rotationUp = Vector3.right;
        [SerializeField] private AnimationCurve speedChangeAnimationCurve;

        private int _direction;
        private float _currentTime;
        private float _currentRotationSpeed;
        private float _currentRotationTime;
        private float _currentMaxRotationSpeed;
        
        private float _minRotationTime;
        private float _maxRotationTime;
        private float _minRotationSpeed;
        private float _maxRotationSpeed;

        private bool _alwaysSwapDirection;
        private float RandomRotationTime => Random.Range(_minRotationTime, _maxRotationTime);
        private float RandomRotationSpeed => Random.Range(_minRotationSpeed, _maxRotationSpeed);
        
        public void OnLevelLoad(LevelData levelData)
        {
            _minRotationTime = levelData.MinRotationTime;
            _maxRotationTime = levelData.MaxRotationTime;
            _minRotationSpeed = levelData.MinRotationSpeed;
            _maxRotationSpeed = levelData.MaxRotationSpeed;
            _alwaysSwapDirection = levelData.AlwaysSwapDirection;
        }
        
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
            if (_alwaysSwapDirection) _direction = -_direction;
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
