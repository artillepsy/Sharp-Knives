using UnityEngine;
using Random = UnityEngine.Random;

namespace ItemThrow
{
    public abstract class AbstractThrower : MonoBehaviour
    {
        [SerializeField] protected float itemMass = 1f;
        [SerializeField] private float minImpulse = 8;
        [SerializeField] private float maxImpulse = 12f; 
        
        [SerializeField] protected float minDeviationAngle = -10f;
        [SerializeField] protected float maxDeviationAngle = 10f;
        protected float _impulse => Random.Range(minImpulse,maxImpulse);
        protected abstract void Throw();

    }
}