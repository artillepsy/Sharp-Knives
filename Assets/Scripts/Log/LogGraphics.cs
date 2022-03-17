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
            material.mainTexture = level.Log.Custom.LogTexture;
            if (!level.Log.Custom.IsBoss) return;
            _particleSystemColor = level.Log.Custom.Boss.ParticlesColor;
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