using Core;
using Knife;
using UnityEngine;

namespace Log
{
    public class LogHitObserver : MonoBehaviour
    {
        public static float LogRadius;

        private void Awake()
        {
            LogRadius = GetComponent<CapsuleCollider>().radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            var comp = other.GetComponentInParent<KnifeStateController>();
            
            if (comp == null) return;
            if (comp.State == KnifeState.Stopped) return;
            
            comp.transform.SetParent(transform);
            comp.SetState(KnifeState.Hitted);
            Events.OnKnifeHit?.Invoke();
            Vibration.VibratePop();
        }

        private void OnCollisionEnter(Collision other)
        {
            var comp = other.collider.GetComponentInParent<KnifeStateController>();
            if (comp == null) return;
            comp.SetState(KnifeState.Stopped);
        }
    }
}