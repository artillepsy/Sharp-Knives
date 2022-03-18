﻿using Scriptable;
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
        public static readonly UnityEvent OnDefeatBoss = new UnityEvent();
        public static readonly UnityEvent<KnifeShopItem> OnUnlock = new UnityEvent<KnifeShopItem>();
        public static readonly UnityEvent<KnifeShopItem> OnEquip = new UnityEvent<KnifeShopItem>();
    }
}