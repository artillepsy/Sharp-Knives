using System.Collections.Generic;
using System.Linq;
using Knife;
using UnityEngine;

namespace Management
{
    public class LogManager : MonoBehaviour
    {
        [SerializeField] private GameObject log;
        [SerializeField] private GameObject brokenLog;
        [SerializeField] private float partMass = 1f;
        [SerializeField] private float knifeMass = 1f;
        
        [SerializeField] private float minImpulse = 8;
        [SerializeField] private float maxImpulse = 12f;
        private float _impulse;
        private List<Rigidbody> _rigidbodies;

        private void OnEnable()
        {
            LevelManager.OnWinGame.AddListener(BreakLog);
        }

        private void Start()
        {
            _impulse = Random.Range(minImpulse, maxImpulse);
            brokenLog.SetActive(false);
            _rigidbodies = brokenLog.GetComponentsInChildren<Rigidbody>().ToList();
        }

        private void BreakLog()
        {
            brokenLog.transform.rotation = log.transform.rotation;
            ThrowKnifes();
            log.SetActive(false);
            brokenLog.SetActive(true);
            ThrowLogParts();
            
        }
        private void ThrowLogParts()
        {
            foreach (var rb in _rigidbodies)
            {
                var direction = (rb.transform.position - log.transform.position).normalized;
                direction = Random.rotation * direction;
                rb.useGravity = true;
                rb.mass = knifeMass;
                rb.AddForceAtPosition(direction * _impulse, log.transform.position, ForceMode.Impulse);
            }
        }
        private void ThrowKnifes()
        {
            var components = FindObjectsOfType<KnifeMovement>();
            foreach (var comp in components)
            {
                comp.transform.SetParent(null);
                comp.enabled = false;
                var direction = (comp.transform.position - log.transform.position).normalized; 
                direction = Random.rotation * direction;
                var rb = comp.GetComponent<Rigidbody>();
                rb.mass = partMass;
                rb.freezeRotation = false;
                rb.AddForceAtPosition(direction * _impulse, log.transform.position, ForceMode.Impulse);
            }
        }
    }
}