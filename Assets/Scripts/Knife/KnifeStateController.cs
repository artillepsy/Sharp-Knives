using System.Collections.Generic;
using System.Linq;
using Core;
using ItemThrow;
using Log;
using Management;
using UI;
using UnityEngine;

namespace Knife
{
    /// <summary>
    /// Класс, отвечающий за контроль состояний ножа
    /// </summary>
    public class KnifeStateController : MonoBehaviour
    {
        private KnifeState _state;
        private BoxCollider _collider;
        private List<IOnKnifeStateChange> _subscribers;
        private CapsuleCollider _logCollider;
        private Vector3 _logPosition;
        private List<AppleThrower> _apples;
        private bool _needCheck = true;
        private void OnEnable()
        {
            Events.OnWinGame?.AddListener(() => GetComponentInChildren<Collider>().enabled = false);
            Events.OnThrow.AddListener(OnTap);
        }
        private void Start()
        {
            _apples = new List<AppleThrower>();
            _collider = GetComponentInChildren<BoxCollider>();
            _state = KnifeState.Ready;
            _logCollider = FindObjectOfType<LogRotation>().GetComponentInChildren<CapsuleCollider>();
            _logPosition = _logCollider.transform.parent.position;
            _subscribers = GetComponents<IOnKnifeStateChange>().ToList();
        }
        /// <summary>
        /// Данный метод проверяет, движется ли нож. Если да, то идёт проверка на расстояние,
        /// на котором он должен остановиться
        /// </summary>
        private void FixedUpdate()
        {
            if (_state != KnifeState.Moving) return;
            if(ShouldStop(out var direction)) Stop(direction);
        }
        /// <summary>
        /// Проверка расстояния от центра ножа до центра бревна. Если оно равно или меньше
        /// нужного, то возвращаемый результат - true
        /// </summary>
        private bool ShouldStop(out Vector3 direction)
        {
            direction = transform.position - _logCollider.transform.parent.position;
            return direction.magnitude <= _logCollider.radius;
        }
        /// <summary>
        /// Остановка ножа. Вызывается один раз для запущенного ножа, если он воткнулся в бревно
        /// </summary>
        private void Stop(Vector3 direction)
        {
            _collider.size = new Vector3(_collider.size.x, 0.6f, _collider.size.z);
            _collider.center = new Vector3(0, -0.20f, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = _logPosition + direction.normalized * _logCollider.radius;
            transform.up = -direction;
            Instantiate(KnifeManager.HitParticleSystem, transform.position, transform.rotation);
            _state = KnifeState.Stopped;
            NotifyAll();
        }
        /// <summary>
        /// Данный метод срабатывает при нажатии на экран. Меняет состояние ножа на "передвижение"
        /// </summary>
        private void OnTap()
        {
            if (_state != KnifeState.Ready) return;
            _state = KnifeState.Moving;
            NotifyAll();
        }
        /// <summary>
        /// Метод, информирующий всех подписчиков о смене состояния ножа. Подписчиками выступают
        /// другие компоненты ножа
        /// </summary>
        private void NotifyAll() => _subscribers.ForEach(sub => sub.OnStateChange(_state));
        /// <summary>
        /// Метод срабатывает при касании ножа до другого коллайдера и проверяет, до чего нож коснулся первым
        /// Пока нож не воткнулся в бревно, идёт проверка на то, не коснулся ли он до яблока
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (_state != KnifeState.Dropped)
            {
                var apple = other.GetComponentInParent<AppleThrower>();
                if (apple && !_apples.Contains(apple))
                {
                    apple.Hit();
                    _apples.Add(apple);
                }
            }
            if (!_needCheck) return;
            if (other.GetComponentInParent<KnifeThrower>())
            {
                _needCheck = false;
                _state = KnifeState.Dropped;
                NotifyAll();
                
                Events.OnKnifeDrop?.Invoke();
                return;
            }
            if (other.GetComponentInParent<LogRotation>())
            {
                _needCheck = false;
                transform.SetParent(other.transform);
                Events.OnKnifeHit?.Invoke();
            }
        }
    }
}