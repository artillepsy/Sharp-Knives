using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Level Data")]
    public class Level : ScriptableObject
    {
        [Header("Level settings")] 
        [Range(0, 1)]
        public float AppleSpawnChance = 0.25f;
        [Range(0, 1)]
        public float KnifeSpawnChance = 1f;


        [Header("Item spawn settings")] 
        public Transform ApplePrefab;
        public Transform KnifePrefab;
        [Space]
        public int MinItemSpawnDistance = 1;
        [Space]
        public int MinSpawnedAppleCount = 1;
        public int MaxSpawnedAppleCount = 2;
        [Space]
        public int MinSpawnedKnifeCount = 1;
        public int MaxSpawnedKnifeCount = 2;

        [Header("Graphics settings")] 
        public Graphics LogGraphics; 
        public Sprite KnifeSprite;
        
        
        [Header("Log rotation settings")]
        public float MinRotationSpeed = 100f;
        public float MaxRotationSpeed = 400f;
        [Space] 
        public float MinRotationTime = 15f;
        public float MaxRotationTime = 25f;
        [Space] 
        public bool AlwaysSwapDirection = false;

        
        [Header("Throwable Knife settings")]
        public int MinKnifeCount = 5;
        public int MaxKnifeCount = 7;
    }
}