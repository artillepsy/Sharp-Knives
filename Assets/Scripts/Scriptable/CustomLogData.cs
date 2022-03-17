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
        }
        [System.Serializable]
        public class BossLog
        {
            [Header("Boss log")]
            public Color ParticlesColor = Color.red;
            public AudioClip HitAudio;
            public AudioClip DestroyAudio;
        }
        
       
    }
}