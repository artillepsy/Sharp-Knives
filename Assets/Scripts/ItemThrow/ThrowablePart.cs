using UnityEngine;

namespace ItemThrow
{
    public class ThrowablePart : MonoBehaviour, IThrowable
    {
        [SerializeField] protected float itemMass = 2f;
        [SerializeField] private bool threeDimensions;
        [Space]
        [SerializeField] private float minImpulse = 15;
        [SerializeField] private float maxImpulse = 25f; 
        [Space]
        [SerializeField] private float minTorque = 3f;
        [SerializeField] private float maxTorque = 6f;
        [Space]
        [SerializeField] protected float minDeviationAngle = -45f;
        [SerializeField] protected float maxDeviationAngle = 45f;
        
        private float _impulse => Random.Range(minImpulse,maxImpulse);
        private float _torque => Random.Range(minTorque, maxTorque);
        private float _deviation => Random.Range(minDeviationAngle, maxDeviationAngle);
        private Quaternion _rotation => Quaternion.Euler(0, 0, _deviation);

        private RigidbodyConstraints GetConstraints()
        {
            if (!threeDimensions)
            {
                return RigidbodyConstraints.FreezeRotationX | 
                       RigidbodyConstraints.FreezeRotationY | 
                       RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                return RigidbodyConstraints.FreezePositionZ;
            }
        }

        private Vector3 GetAxis()
        {
            if (threeDimensions) return Random.rotation * Vector3.forward;
            return Random.value > 0.5f ? Vector3.forward : -Vector3.forward;
        }
        
        public void Throw(Vector3 direction)
        {
            var rb = GetComponent<Rigidbody>();
            rb.transform.SetParent(null);
            rb.mass = itemMass;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.constraints = GetConstraints();
            direction = _rotation * direction;
            rb.AddForce(direction * _impulse, ForceMode.Impulse);
            rb.AddTorque(GetAxis() * _torque, ForceMode.Impulse);
        }
    }
}