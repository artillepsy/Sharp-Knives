using System;
using Scriptable;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Log
{
    /// <summary>
    /// Класс, отвечающий за вращение бревна
    /// </summary>
    public class LogRotation : MonoBehaviour, IOnLevelLoad
    {
        [Header("Rotation")] 
        [SerializeField] private Transform child;
        [SerializeField] private Vector3 rotationUp = Vector3.right;
        [SerializeField] private AnimationCurve accelerationAnimCurve;
        [SerializeField] private AnimationCurve stopAnimCurve;
        private RotationState _state = RotationState.Stopped;
        private Action _action;
        private Level _level;
        private int _direction;
        private float _currentTime;
        private float _maxTime;
        private float _maxRotationSpeed;
        /// <summary>
        /// Перечисление состояний вращения
        /// </summary>
        private enum RotationState
        {
            Accelerating, // ускорение
            Stopping, // замедление
            Stopped, // остановка
            Rotating // вращение
        }
        public void OnLevelLoad(Level level) => _level = level;
        /// <summary>
        /// Текущий метод инициализирует начальное направление бревна и состояние вращения
        /// </summary>
        private void Start()
        {
            _direction = Random.value > 0.5f ? 1 : -1;
            _action = null;
            ChangeState();
        }
        /// <summary>
        /// Текущий метод проверяет, нужно ли изменять состояние вращеня,
        /// и вызывает характерный для текущего состояния метод
        /// </summary>
        private void Update()
        {
            if (NeedChangeState()) ChangeState();
            _action?.Invoke();
        }
        /// <summary>
        /// Метод изменения сосояния вращения бревна. Здесь устанавливается длительность состояния,
        /// скорость вращения, направление вращения и само состояние
        /// </summary>
        private void ChangeState()
        {
            if (!_level) return;
            switch (_state)
            {
                case RotationState.Stopped:
                    _maxTime = Random.Range(_level.Log.MinAccelerationTime, _level.Log.MaxAccelerationTime);
                    _maxRotationSpeed = Random.Range(_level.Log.MinRotationSpeed, _level.Log.MaxRotationSpeed);
                    if (_level.Log.AlwaysSwapDirection) _direction = -_direction;
                    else _direction = Random.value > 0.5f ? 1 : -1;
                    _maxRotationSpeed *= _direction;
                    _action = Accelerate;
                    _state = RotationState.Accelerating;
                    break;
                case RotationState.Accelerating:
                    _maxTime = Random.Range(_level.Log.MinRotationTime, _level.Log.MaxRotationTime);
                    _action = Rotate;
                    _state = RotationState.Rotating;
                    break;
                case RotationState.Rotating:
                    _maxTime = Random.Range(_level.Log.MinStoppingTime, _level.Log.MaxStoppingTime);
                    _action = Stop;
                    _state = RotationState.Stopping;
                    break;
                case RotationState.Stopping:
                    _maxTime = Random.Range(_level.Log.MinStoppedTime, _level.Log.MaxStoppedTime);
                    _action = null;
                    _state = RotationState.Stopped;
                    break;
            }
            _currentTime = 0f;
        }
        /// <summary>
        /// Проверка на то, закончилось ли время текущего состояния
        /// </summary>
        private bool NeedChangeState()
        {
            if (_currentTime >= _maxTime) return true;
            _currentTime += Time.deltaTime;
            return false;
        }
        /// <summary>
        /// Метод, вызываемый в Update при ускорении
        /// </summary>
        private void Accelerate()
        {
            var rotationSpeed = accelerationAnimCurve.Evaluate(_currentTime / _maxTime);
            rotationSpeed *= _maxRotationSpeed;
            child.Rotate(rotationUp, rotationSpeed*Time.deltaTime);
        }
        /// <summary>
        /// Метод, вызываемый в Update при замедлении
        /// </summary>
        private void Stop()
        {
            var rotationSpeed = stopAnimCurve.Evaluate(_currentTime / _maxTime);
            rotationSpeed *= _maxRotationSpeed;
            child.Rotate(rotationUp, rotationSpeed*Time.deltaTime);
        }
        /// <summary>
        /// Метод, вызываемый в Update при вращении
        /// </summary>
        private void Rotate() => child.Rotate(rotationUp, _maxRotationSpeed*Time.deltaTime);
    }
}
