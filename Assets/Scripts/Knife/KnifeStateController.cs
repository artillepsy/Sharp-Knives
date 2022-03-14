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
        private List<IOnKnifeStateChange> _subscribers;
        private float _logRadius;
        private Vector3 _logPosition;
        private bool _needCheck = true;

        private void OnEnable()
        {
            Events.OnTap.AddListener(OnTap);
        }

        private void Start()
        {
            var comp = FindObjectOfType<LogRotation>();
            _logRadius = comp.GetComponent<CapsuleCollider>().radius;
            _logPosition = comp.transform.position;
            Vibration.Init();
            SubscribeComponents();
            _state = KnifeState.Ready;
        }

        private void FixedUpdate()
        {
            if (_state != KnifeState.Moving) return;
            var direction = transform.position - _logPosition;
            if (direction.magnitude > _logRadius) return;
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            transform.position = _logPosition + direction.normalized * _logRadius;
            transform.up = -direction;
            
            Instantiate(KnifeManager.HitParticleSystem, transform.position, transform.rotation);
            
            _state = KnifeState.Stopped;
            NotifyAll(_state);
        }

        private void OnTap()
        {
            if (_state != KnifeState.Ready) return;
            _state = KnifeState.Moving;
            NotifyAll(_state);
        }

        private void SubscribeComponents()
        {
            _subscribers = GetComponents<IOnKnifeStateChange>().ToList();
        }
        
        private void NotifyAll(KnifeState newState)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnStateChange(newState);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_needCheck) return;
            var comp = other.GetComponent<LogRotation>();
            if (comp)
            {
                _needCheck = false;
                transform.SetParent(comp.transform);
                Events.OnKnifeHit?.Invoke();
                Vibration.VibratePop();
                _logRadius = comp.GetComponent<CapsuleCollider>().radius;
                return;
            }
            if (other.GetComponentInParent<KnifeThrower>())
            {
                _needCheck = false;
                _state = KnifeState.Dropped;
                NotifyAll(_state);
                Vibration.VibratePop();
                Events.OnKnifeDrop?.Invoke();
                return;
            }
            other.GetComponentInParent<AppleThrower>()?.HitApple();
        }

        
    }
}