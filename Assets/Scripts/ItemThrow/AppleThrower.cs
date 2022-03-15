using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace ItemThrow
{
    public class AppleThrower : MonoBehaviour
    {
        [SerializeField] private GameObject fullApple;
        [SerializeField] private List<GameObject> appleParts;
        private List<ThrowablePart> _parts;

        public void Hit()
        {
            transform.SetParent(null);
            fullApple.SetActive(false);
            appleParts.ForEach(part => part.SetActive(true));
            _parts.ForEach(comp => comp.Throw(Vector3.up));
            Events.OnAppleHit?.Invoke();
            Events.OnWinGame.RemoveListener(ThrowParts);
        }
        private void OnEnable()
        {
            _parts = GetComponentsInChildren<ThrowablePart>(true).ToList();
            Events.OnWinGame.AddListener(ThrowParts);
        }
        private void ThrowParts() => _parts.ForEach(comp => comp.Throw(Vector3.up));
    }
}