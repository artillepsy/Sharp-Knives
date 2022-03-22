using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace ItemThrow
{
    /// <summary>
    /// Класс, отвечающий за бросание яблока (при попадании ножа или при успешном прохождении уровня)
    /// </summary>
    public class AppleThrower : MonoBehaviour
    {
        [SerializeField] private GameObject fullApple;
        [SerializeField] private List<GameObject> appleParts;
        private List<ThrowablePart> _parts;
        /// <summary>
        /// Метод, срабатывающий при попадании по яблоку ножа. Отвечает за активизацию яблочных долек и их бросок.
        /// </summary>
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
        /// <summary>
        /// Метод, срабатывающий при успешном завершении уровня. Бросает целое яблоко
        /// </summary>
        private void ThrowParts() => _parts.ForEach(comp => comp.Throw(Vector3.up));
    }
}