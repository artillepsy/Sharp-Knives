using System.Collections.Generic;
using System.Linq;
using Core;
using Log;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ItemThrow
{
    public class LogPartThrower : AbstractThrower
    {
        private GameObject _log;
        private List<Rigidbody> _logPartsRbList;

        private void OnEnable()
        {
            Events.OnWinGame.AddListener(Throw);
        }

        private void Start()
        {
            _log = FindObjectOfType<LogRotation>().gameObject;
            _logPartsRbList = GetComponentsInChildren<Rigidbody>().ToList();

            foreach (var rb in _logPartsRbList)
            {
                rb.gameObject.SetActive(false);
            }
        }

        private Quaternion RandomRotation()
        {
            var x = Random.Range(minDeviationAngle, maxDeviationAngle);
            var y = Random.Range(minDeviationAngle, maxDeviationAngle);
            return Quaternion.Euler(x, y, 0);
        }
        
        protected override void Throw()
        {
            transform.rotation = _log.transform.rotation;
            
            _log.SetActive(false);
            
            
            foreach (var rb in _logPartsRbList)
            {
                rb.gameObject.SetActive(true);
                var direction = (rb.transform.position -transform.position).normalized;
                direction = RandomRotation() * direction;
                rb.useGravity = true;
                rb.mass = base.itemMass;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                rb.AddForceAtPosition(direction * base._impulse, transform.position, ForceMode.Impulse);
            }
        }
    }
}