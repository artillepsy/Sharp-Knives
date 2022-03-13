using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace ItemThrow
{
    public class AppleThrower : ItemThrower
    {
        private List<Rigidbody> _children;

        public void HitApple()
        {
            foreach (var rb in _children)
            {
                var activeStatus = !rb.GetComponent<Collider>();
                rb.gameObject.SetActive(activeStatus);
            }

            Throw();
            Events.OnAppleHit?.Invoke();
        }
        
        private void Awake()
        {
            _children = GetComponentsInChildren<Rigidbody>().ToList();
            foreach (var rb in _children)
            {
                if(!rb.GetComponent<Collider>()) rb.gameObject.SetActive(false);
            }
        }

        protected override void Throw()
        {
            foreach (var rb in _children)
            {
                if(!rb.gameObject.activeSelf) continue;
                ThrowSingleRigidbody(rb);
            }
        }
        
    }
}