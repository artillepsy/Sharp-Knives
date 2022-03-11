using Knife;
using UnityEngine;

namespace Log
{
    public class LogStickness : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var comp = other.GetComponentInParent<KnifeMovement>();
            
            if (comp == null) return;
            if (comp.State == MovementState.Stopped) return;
            
            comp.transform.SetParent(transform);
            comp.State = MovementState.Sticked;
        }

        private void OnCollisionEnter(Collision other)
        {
            var comp = other.collider.GetComponentInParent<KnifeMovement>();
            if (comp == null) return;
            comp.State = MovementState.Stopped;
        }
    }
}