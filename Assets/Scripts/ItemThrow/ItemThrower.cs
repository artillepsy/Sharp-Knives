using Core;
using UnityEngine;

namespace ItemThrow
{
    public class ItemThrower : AbstractThrower // to knifes in log, player's knifes, apples?
    {
        [SerializeField] private float minTorqueImpulse = 3f;
        [SerializeField] private float maxTorqueImpulse = 6f;
        private Quaternion _randomRotation => Quaternion.Euler(0, 0, Random.Range(minDeviationAngle, maxDeviationAngle));
        private float _randomTorqueImpulse => Random.Range(minTorqueImpulse, maxTorqueImpulse);
        protected Vector3 _direction = Vector3.up;
        
        private void OnEnable()
        {
            Events.OnWinGame.AddListener(Throw);
        }

        protected override void Throw()
        {
            transform.SetParent(null);
            var rb = GetComponent<Rigidbody>();
            rb.mass = base.itemMass;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

            var direction = _randomRotation * _direction;
            rb.AddForce(direction * base._impulse, ForceMode.Impulse);

            var axis = Random.value > 0.5f ? Vector3.forward : -Vector3.forward;
            rb.AddTorque(axis * _randomTorqueImpulse, ForceMode.Impulse);
        }
    }
}