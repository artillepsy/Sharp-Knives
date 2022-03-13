using System.Collections.Generic;
using System.Linq;
using Log;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ItemThrow
{
    public class LogPartThrower : AbstractThrower
    {
        private GameObject _log;
        private List<Rigidbody> _logPartsRbs;
        private void Start()
        {
            _log = FindObjectOfType<LogRotation>().gameObject;
            _logPartsRbs = GetComponentsInChildren<Rigidbody>().ToList();
            foreach (var rb in _logPartsRbs)
            {
                rb.gameObject.SetActive(false);
            }
        }
        protected override void Throw()
        {
            transform.rotation = _log.transform.rotation;
            _log.SetActive(false);
            foreach (var rb in _logPartsRbs)
            {
                rb.gameObject.SetActive(true);
                var direction = (rb.transform.position -transform.position).normalized;
                direction = Random.rotation * direction;
                rb.useGravity = true;
                rb.mass = base.itemMass;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.AddForceAtPosition(direction * base._impulse, transform.position, ForceMode.Impulse);
            }
        }
    }
}