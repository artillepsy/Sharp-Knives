using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Log Graphics")]
    public class CustomLogData : ScriptableObject
    {
        public bool IsBoss = false;
        public Texture2D LogTexture;
        public Color HitParticleColor = new Color(0xE5, 0xA5, 0x60);
        public DefaultLog Default;
        public BossLog Boss;

        [System.Serializable]
        public class DefaultLog
        {
            [Header("Default log")]
            public Color InsideColor = new Color(0xE5, 0xA5, 0x60);
            public int MinWinCount = 0;
            public int MaxWinCount = 4;
        }
        [System.Serializable]
        public class BossLog
        {
            [Header("Boss log")] 
            [Range(0, 100)]
            public int DamageAtDestroy = 20;
            [Range(0, 1)] 
            public float Chance = 0.3f;
            public Color ParticlesColor = Color.red;
            public List<AudioClip> HitClips;
            public List<AudioClip> DestroyClips;
        }
    }
}