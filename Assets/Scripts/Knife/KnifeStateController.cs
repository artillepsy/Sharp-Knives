using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Log;
using UnityEngine;
using UnityEngine.Events;

namespace Knife
{
    public class KnifeStateController : MonoBehaviour
    {
        private KnifeState _state;
        public KnifeState State => _state;
        private List<IOnKnifeStateChange> _subscribers;
        public static UnityEvent OnDrop = new UnityEvent();

        public void SetState(KnifeState newState)
        {
            _state = newState;
            NotifyAll(_state);
        }

        private void Awake()
        {
            AddSubscribers();
            _state = KnifeState.Ready;
        }

        private void OnEnable()
        {
            TouchInput.OnTouch.AddListener(OnTouch);
        }

        private void OnTouch()
        {
            _state = KnifeState.Moving;
            NotifyAll(_state);
        }

        private void AddSubscribers()
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

        private void OnCollisionEnter(Collision other)
        {
            if (_state != KnifeState.Moving) return;
            var comp = other.gameObject.GetComponent<KnifeStateController>();
            if (comp == null) return;
            if (comp.State != KnifeState.Moving) return;
            OnDrop?.Invoke();
            _state = KnifeState.Dropped;
            NotifyAll(_state);
        }
    }
}