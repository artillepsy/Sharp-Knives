using UnityEngine;

namespace Scriptable
{
    /// <summary>
    /// Класс, хранящий в себе всю информацию об уровне
    /// </summary>
    [CreateAssetMenu(fileName = "Level Data")]
    public class Level : ScriptableObject
    {
        [Header("Appearance settings")]
        public int MinWinCount = 0;
        public int MaxWinCount = 4;
        public ItemsSettings Items;
        public LogSettings Log;
        public KnifeSettings Knife;

        /// <summary>
        /// Класс настройки шанса выпадения и количества объектов
        /// </summary>
        [System.Serializable]
        public class ItemsSettings
        {
            [Header("Spawn chance")] 
            [Range(0, 1)]
            public float AppleSpawnChance = 0.25f;
            [Range(0, 1)]
            public float KnifeSpawnChance = 1f;
            
            
            [Header("Item spawn settings")] 
            public int MinSpawnDistance = 1;
            [Space]
            public int MinAppleCount = 1;
            public int MaxAppleCount = 2;
            [Space]
            public int MinKnifeCount = 1;
            public int MaxKnifeCount = 2;
        }
        /// <summary>
        /// Класс настройки графики и аудио бревна, а также его вращения
        /// </summary>
        [System.Serializable]
        public class LogSettings
        {
            [Header("Log graphics settings")] 
            public CustomLogSettings Settings;
            
            
            [Header("Log rotation settings")]
            public float MinRotationSpeed = 100f;
            public float MaxRotationSpeed = 400f;
            [Space] 
            public float MinAccelerationTime = 3f;
            public float MaxAccelerationTime = 5f;
            [Space] 
            public float MinRotationTime = 3f;
            public float MaxRotationTime = 5f;
            [Space] 
            public float MinStoppingTime = 3f;
            public float MaxStoppingTime = 5f;
            [Space]
            public float MinStoppedTime = 3f;
            public float MaxStoppedTime = 5f;
            [Space] 
            public bool AlwaysSwapDirection = false;
        }
        /// <summary>
        /// Класс настройки количества запускаемых ножей на уровне
        /// </summary>
        [System.Serializable]
        public class KnifeSettings
        {
            [Header("Throwable Knife settings")]
            public int MinCount = 5;
            public int MaxCount = 7;
        }
    }
}