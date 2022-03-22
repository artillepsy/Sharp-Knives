using System.Collections.Generic;
using UnityEngine;

namespace Scriptable
{
    /// <summary>
    /// Класс, хранщий графические и аудио настройки бревна
    /// </summary>
    [CreateAssetMenu(fileName = "Log Settings")]
    public class CustomLogSettings : ScriptableObject
    {
        public bool IsBoss = false;
        public Texture2D LogTexture;
        public Color HitParticleColor = new Color(0xE5, 0xA5, 0x60);
        public DefaultLog Default;
        public BossLog Boss;

        /// <summary>
        /// Класс, хранящий информацию о бревне на обычном уровне
        /// </summary>
        [System.Serializable]
        public class DefaultLog
        {
            [Header("Default log")]
            public Color InsideColor = new Color(0xE5, 0xA5, 0x60);
        }
        /// <summary>
        /// класс, хранящий информацию на уровне с боссом
        /// </summary>
        [System.Serializable]
        public class BossLog
        {
            [Header("Boss log")] 
            public string Name = "Tomato";
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