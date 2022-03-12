using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ItemThrow
{
    public class LogPartThrower : ItemThrower
    {
        [SerializeField] private GameObject log;
        [SerializeField] private GameObject brokenLog;
        private List<Rigidbody> _logPartsRbList;

        private void OnEnable()
        {
            Events.OnWinGame.AddListener(Throw);
        }

        private void Start()
        {
            brokenLog.SetActive(false);
            _logPartsRbList = brokenLog.GetComponentsInChildren<Rigidbody>().ToList();
        }

        private Quaternion RandomRotation()
        {
            var x = Random.Range(minDeviationAngle, maxDeviationAngle);
            var y = Random.Range(minDeviationAngle, maxDeviationAngle);
            return Quaternion.Euler(x, y, 0);
        }
        
        protected override void Throw()
        {
            brokenLog.transform.rotation = log.transform.rotation;
            log.SetActive(false);
            brokenLog.SetActive(true);
            
            foreach (var rb in _logPartsRbList)
            {
                var direction = (rb.transform.position - brokenLog.transform.position).normalized;
                direction = RandomRotation() * direction;
                rb.useGravity = true;
                rb.mass = base.itemMass;
                rb.AddForceAtPosition(direction * base._impulse, brokenLog.transform.position, ForceMode.Impulse);
            }
        }
    }
}