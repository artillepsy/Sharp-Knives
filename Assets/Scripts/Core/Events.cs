using UnityEngine.Events;

namespace Core
{
    public static class Events
    {
        public static readonly UnityEvent OnTap = new UnityEvent();
        public static readonly UnityEvent OnKnifeDrop = new UnityEvent();
        public static readonly UnityEvent OnKnifeHit = new UnityEvent();
        public static readonly UnityEvent OnAppleHit = new UnityEvent();
        public static readonly UnityEvent OnWinGame = new UnityEvent();
        public static readonly UnityEvent OnFailGame = new UnityEvent();
        public static readonly UnityEvent<int> OnBuy = new UnityEvent<int>();
        public static readonly UnityEvent<int> OnEquip = new UnityEvent<int>();
    }
}