using Core;
using Knife;
using UnityEngine;

namespace ItemThrow
{
    /// <summary>
    /// КЛасс, отвечающий за отбрасывание ножа (в результате победы или поражения)
    /// </summary>
    public class KnifeThrower : MonoBehaviour, IOnKnifeStateChange
    {
        private ThrowablePart _part;
        /// <summary>
        /// Здесь идёт подписка на событие победы, в случае которой ножик подбрасывается
        /// </summary>
        private void OnEnable()
        {
            _part = GetComponent<ThrowablePart>();
            Events.OnWinGame.AddListener(() => _part.Throw(Vector3.up));
        }
        /// <summary>
        /// В данном методе идёт проверка состояния ножика. Если состояние эквивалентно тому, что ножик ударился, то
        /// идёт бросок ножа вниз
        /// </summary>
        public void OnStateChange(KnifeState newState)
        {
            if (newState != KnifeState.Dropped) return;
            
            _part.Throw(Vector3.down);
        }
    }
}