using Core;
using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogGraphics : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private ParticleSystem bossParticleSystem;
        private Color _particleSystemColor;
        public void OnLevelLoad(Level level)
        {
            var material = GetComponent<MeshRenderer>().sharedMaterial;
            material.mainTexture = level.GraphicsData.LogTexture;
            if (!level.GraphicsData.IsBoss) return;
            _particleSystemColor = level.GraphicsData.BossParticlesColor;
            Events.OnWinGame.AddListener(SpawnParticles);
        }

        private void SpawnParticles()
        {
            var instance = Instantiate(bossParticleSystem, transform.position, Quaternion.identity);
            var main = instance.main;
            main.startColor = _particleSystemColor;
        }
    }
}