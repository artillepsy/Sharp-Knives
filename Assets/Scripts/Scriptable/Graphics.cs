using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "Log Graphics")]
    public class Graphics : ScriptableObject
    {
        public bool IsBoss = false;
        public Texture2D LogTexture;
        [Header("Standart log")]
        public Color PartsInsideColor = new Color(0xE5, 0xA5, 0x60);
        [Header("Boss log")]
        public Color BossParticlesColor = Color.red;
        public Color HitParticleColor = new Color(0xE5, 0xA5, 0x60);
    }
}