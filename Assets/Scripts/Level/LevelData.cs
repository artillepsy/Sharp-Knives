using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Level Data")]
    public class LevelData : ScriptableObject
    {
        [Header("Level settings")] 
        public bool IsBoss = false;
        [Range(0, 1)]
        public float AppleSpawnChance = 0.25f;
        [Range(0, 1)]
        public float KnifeSpawnChance = 1f;


        [Header("Item spawn settings")] 
        public float minApplePlacementAngle = 10f;
        public float maxApplePlacementAngle = 10f;
        public int MinSpawnedAppleCount = 1;
        public int MaxSpawnedAppleCount = 2;
        [Space]
        public float minKnifePlacementAngle = 10f;
        public float maxKnifePlacementAngle = 10f;
        public int MinSpawnedKnifeCount = 1;
        public int MaxSpawnedKnifeCount = 2;
        
        
        [Header("Graphics settings")]
        public Texture2D LogTexture;
        public Sprite KnifeSprite;
        [Space]
        public Color InsideLogPartsColor;
        public Color BossParticleColor;
        public Color KnifeHitColor;
        
        
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