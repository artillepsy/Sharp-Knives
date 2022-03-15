using System.Collections.Generic;
using System.Linq;
using Core;
using ItemThrow;
using Log;
using Management;
using UnityEngine;

namespace Knife
{
    public class KnifeStateController : MonoBehaviour
    {
        private KnifeState _state;
        private BoxCollider _collider;
        private List<IOnKnifeStateChange> _subscribers;
        private CapsuleCollider _logCollider;
        private Vector3 _logPosition;
        private bool _needCheck = true;
        private void OnEnable()
        {
            Events.OnWinGame?.AddListener(() => GetComponentInChildren<Collider>().enabled = false);
            Events.OnTap.AddListener(OnTap);
        }

        private void Start()
        {
            Vibration.Init();
            _collider = GetComponentInChildren<BoxCollider>();
            _state = KnifeState.Ready;
            _logCollider = FindObjectOfType<LogRotation>().GetComponentInChildren<CapsuleCollider>();
            _logPosition = _logCollider.transform.parent.position;
            _subscribers = GetComponents<IOnKnifeStateChange>().ToList();
        }

        private void FixedUpdate()
        {
            if (_state != KnifeState.Moving) return;
            if(ShouldStop(out var direction)) Stop(direction);
        }

        private bool ShouldStop(out Vector3 direction)
        {
            direction = transform.position - _logCollider.transform.parent.position;
            return direction.magnitude <= _logCollider.radius;
        }

        private void Stop(Vector3 direction)
        {
            _collider.size = new Vector3(1, 0.6f, 1);
            _collider.center = new Vector3(0, -0.20f, 0);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.position = _logPosition + direction.normalized * _logCollider.radius;
            transform.up = -direction;
            Instantiate(KnifeManager.HitParticleSystem, transform.position, transform.rotation);
            _state = KnifeState.Stopped;
            NotifyAll();
        }

        private void OnTap()
        {
            if (_state != KnifeState.Ready) return;
            _state = KnifeState.Moving;
            NotifyAll();
        }
        private void NotifyAll() => _subscribers.ForEach(sub => sub.OnStateChange(_state));
        private void OnTriggerEnter(Collider other)
        {
            if(_state != KnifeState.Dropped) other.GetComponentInParent<AppleThrower>()?.Hit();
            if (!_needCheck) return;
            if (other.GetComponentInParent<KnifeThrower>())
            {
                _needCheck = false;
                _state = KnifeState.Dropped;
                NotifyAll();
                Vibration.VibratePop();
                Events.OnKnifeDrop?.Invoke();
                return;
            }
            if (other.GetComponentInParent<LogRotation>())
            {
                _needCheck = false;
                transform.SetParent(other.transform);
                Events.OnKnifeHit?.Invoke();
                Vibration.VibratePop();
                return;
            }
        }
    }
}