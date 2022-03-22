using System;
using Core;
using UnityEngine;

namespace Knife
{
    /// <summary>
    /// Класс, отвечающий за передвижение ножика
    /// </summary>
    public class KnifeMovement : MonoBehaviour, IOnKnifeStateChange
    {
        [SerializeField] private float speed;
        private Rigidbody _rb;
        private Action _action;
        private void Start() => _rb = GetComponent<Rigidbody>();
        /// <summary>
        /// В данном методе вызывается действие, присвоенное переменной в зависимости
        /// от состояния ножа
        /// </summary>
        private void FixedUpdate() => _action?.Invoke();
        /// <summary>
        /// Данный метод отвечает за движение ножа после запуска
        /// </summary>
        private void Move() => _rb.velocity = transform.up * speed;
        /// <summary>
        /// Данный метод отвечает за изменение передвижения ножа в зависимости от текущего состояния
        /// </summary>
        public void OnStateChange(KnifeState newState)
        {
            switch (newState)
            {
                case KnifeState.Moving:
                    _rb.constraints = RigidbodyConstraints.FreezeRotation;
                    _action = Move;
                    break;
                case KnifeState.Stopped:
                    _action = null;
                    _rb.velocity = Vector3.zero;
                    break;
                case KnifeState.Dropped:
                    _action = null;
                    break;
            }
        }
    }
}