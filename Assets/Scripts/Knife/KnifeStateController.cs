using System.Collections.Generic;
using System.Linq;
using Core;
using Log;
using UnityEngine;

namespace Knife
{
    public class KnifeStateController : MonoBehaviour
    {
        private KnifeState _state;
        public KnifeState State => _state;
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

        private void OnTap()
        {
            if (_state != KnifeState.Ready) return;
            _state = KnifeState.Moving;
            NotifyAll(_state);
        }

        private void FixedUpdate()
        {
            if (_state != KnifeState.Moving) return;
            var direction = transform.position - _logPosition;
            if (direction.magnitude > _logRadius) return;
            _state = KnifeState.Stopped;
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
            if (other.GetComponent<LogRotation>())
            {
                _needCheck = false;
                transform.SetParent(other.transform);
                Events.OnKnifeHit?.Invoke();
                Vibration.VibratePop();
                return;
            }
            if (!other.GetComponentInParent<KnifeMovement>()) return;
            _needCheck = false;
            Vibration.VibratePop();
            Events.OnKnifeDrop?.Invoke();
            
        }
    }
}