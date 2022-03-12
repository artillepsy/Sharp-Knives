using Core;
using Knife;
using UnityEngine;

namespace ItemThrow
{
    public class KnifeThrower : ItemThrower
    {
        [SerializeField] private float minTorqueImpulse = 3f;
        [SerializeField] private float maxTorqueImpulse = 6f;
        private Quaternion _randomRotation => Quaternion.Euler(0, 0, Random.Range(minDeviationAngle, maxDeviationAngle));
        private float _randomTorqueImpulse => Random.Range(minTorqueImpulse, maxTorqueImpulse);
        private void OnEnable()
        {
            Events.OnWinGame.AddListener(Throw);
        }
        protected override void Throw()
        {
            var components = FindObjectsOfType<KnifeMovement>();
            foreach (var comp in components)
            {
                comp.transform.SetParent(null);
                comp.enabled = false;

                comp.GetComponentInChildren<Collider>().enabled = false;
                
                var rb = comp.GetComponent<Rigidbody>();
                rb.mass = base.itemMass;
                rb.useGravity = true;
                rb.velocity = Vector3.zero;
                rb.freezeRotation = false;
                rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

                var direction = _randomRotation * Vector3.up;
                rb.AddForce(direction * base._impulse, ForceMode.Impulse);

                var axis = Random.value > 0.5f ? Vector3.forward : -Vector3.forward;
                rb.AddTorque(axis * _randomTorqueImpulse, ForceMode.Impulse);
            }
        }
    }
}