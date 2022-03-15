using System.Collections.Generic;
using System.Linq;
using Core;
using Log;
using UnityEngine;

namespace ItemThrow
{
    public class LogPartThrower : MonoBehaviour
    {
        private GameObject _log;
        private List<ThrowablePart> _parts;
        private void OnEnable()
        {
            _parts = GetComponentsInChildren<ThrowablePart>().ToList();
            _parts.ForEach(part => part.gameObject.SetActive(false));
            _log = FindObjectOfType<LogRotation>().gameObject;
            Events.OnWinGame.AddListener(ThrowParts);
        }
        private void ThrowParts()
        {
            transform.rotation = _log.transform.rotation;
            _log.SetActive(false);
            _parts.ForEach(part => part.gameObject.SetActive(true));
            _parts.ForEach(part => part.Throw(part.transform.position - _log.transform.position));
        }
    }
}