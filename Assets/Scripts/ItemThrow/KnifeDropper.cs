using Core;
using Knife;
using UnityEngine;

namespace ItemThrow
{
    public class KnifeDropper : ItemThrower
    {
        [SerializeField] private float minTorqueImpulse = 3f;
        [SerializeField] private float maxTorqueImpulse = 6f;
        
        private Quaternion _randomRotation => Quaternion.Euler(0, 0, Random.Range(minDeviationAngle, maxDeviationAngle));
        private float _randomTorqueImpulse => Random.Range(minTorqueImpulse, maxTorqueImpulse);
        
        private void OnEnable()
        {
            Events.OnKnifeDrop.AddListener(Throw);
        }

        protected override void Throw()
        {
            var components = FindObjectsOfType<KnifeStateController>();

            foreach (var comp in components)
            {
                switch (comp.State)
                {
                    case KnifeState.Ready:
                        Destroy(comp.gameObject);
                        break;
                    case KnifeState.Dropped:
                        var rb = comp.GetComponent<Rigidbody>();
                        DropKnife(rb);
                        break;
                    case KnifeState.Moving:
                        Destroy(comp.gameObject);
                        break;
                    default: break;
                }
            }
        }

        private void DropKnife(Rigidbody rb)
        {
            rb.mass = base.itemMass;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = false;
                
            var direction = _randomRotation * Vector3.down;
            rb.AddForce(direction * base._impulse, ForceMode.Impulse);

            var axis = Random.value > 0.5f ? Vector3.forward : -Vector3.forward;
            rb.AddTorque(axis * _randomTorqueImpulse, ForceMode.Impulse);
        }
        
    }
}