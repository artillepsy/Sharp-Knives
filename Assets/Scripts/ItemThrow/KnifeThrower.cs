using Core;
using Knife;
using UnityEngine;

namespace ItemThrow
{
    public class KnifeThrower : MonoBehaviour, IOnKnifeStateChange
    {
        private ThrowablePart _part;
        private void OnEnable()
        {
            _part = GetComponent<ThrowablePart>();
            Events.OnWinGame.AddListener(() => _part.Throw(Vector3.up));
        }

        public void OnStateChange(KnifeState newState)
        {
            if (newState != KnifeState.Dropped) return;
            
            _part.Throw(Vector3.down);
        }
    }
}