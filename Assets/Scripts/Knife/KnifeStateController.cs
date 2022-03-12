using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Knife
{
    public class KnifeStateController : MonoBehaviour
    {
        private KnifeState _state;
        public KnifeState State => _state;
        private List<IOnKnifeStateChange> _subscribers;

        public void SetState(KnifeState newState)
        {
            _state = newState;
            NotifyAll(_state);
        }

        private void Awake()
        {
            Vibration.Init();
            SubscribeComponents();
            _state = KnifeState.Ready;
        }

        private void OnEnable()
        {
            Events.OnTap.AddListener(OnTap);
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

        private void OnCollisionEnter(Collision other)
        {
            if (_state != KnifeState.Moving) return;
            
            var comp = other.gameObject.GetComponent<KnifeStateController>();
            if (comp == null) return;
            if (comp.State != KnifeState.Stopped) return;
            Debug.Log("dropped: "+ _state );
            _state = KnifeState.Dropped;
           
            NotifyAll(_state);
            Vibration.VibratePop();
            Events.OnKnifeDrop?.Invoke();
        }
    }
}