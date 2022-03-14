using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ItemThrow
{
    public abstract class AbstractThrower : MonoBehaviour
    {
        [SerializeField] protected float itemMass = 2f;
        [SerializeField] private float minImpulse = 15;
        [SerializeField] private float maxImpulse = 25f; 
        [SerializeField] protected float minDeviationAngle = -45f;
        [SerializeField] protected float maxDeviationAngle = 45f;
        protected float _impulse => Random.Range(minImpulse,maxImpulse);
        protected abstract void Throw();
        private void OnEnable()
        {
            Events.OnWinGame.AddListener(Throw);
        }
    }
}