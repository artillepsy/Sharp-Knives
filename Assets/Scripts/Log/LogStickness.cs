using Knife;
using UnityEngine;

namespace Log
{
    public class LogStickness : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var comp = other.GetComponentInParent<KnifeStateController>();
            
            if (comp == null) return;
            if (comp.State == KnifeState.Stopped) return;
            
            comp.transform.SetParent(transform);
            comp.SetState(KnifeState.Sticked);
        }

        private void OnCollisionEnter(Collision other)
        {
            var comp = other.collider.GetComponentInParent<KnifeStateController>();
            if (comp == null) return;
            comp.SetState(KnifeState.Stopped);
        }
    }
}