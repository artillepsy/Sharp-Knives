using UnityEngine;

namespace Scriptable
{
    public abstract class Graphics : ScriptableObject
    {
        public bool IsBoss = false;
        public Texture2D LogTexture;
        public Color HitParticleColor;
    }
}