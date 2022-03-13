using Core;
using Knife;
using UnityEngine;

namespace ItemThrow
{
    public class KnifeThrower : ItemThrower, IOnKnifeStateChange
    {
        public void OnStateChange(KnifeState newState)
        {
            if (newState != KnifeState.Dropped) return;
            _direction = Vector3.down;
            Throw();
        }
        
        private void Awake()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}