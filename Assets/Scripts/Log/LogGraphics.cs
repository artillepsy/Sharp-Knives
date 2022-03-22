using Core;
using Scriptable;
using UnityEngine;

namespace Log
{
    /// <summary>
    /// Класс, инициализирующий графику бревна в начале уровня
    /// </summary>
    public class LogGraphics : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private ParticleSystem bossParticleSystem;
        private Color _particleSystemColor;
        /// <summary>
        /// Изменение материала бревна и системы частиц при уничтожении, если это босс
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            var material = GetComponent<MeshRenderer>().sharedMaterial;
            material.mainTexture = level.Log.Settings.LogTexture;
            if (!level.Log.Settings.IsBoss) return;
            _particleSystemColor = level.Log.Settings.Boss.ParticlesColor;
            Events.OnWinGame.AddListener(SpawnParticles);
        }
        /// <summary>
        /// Если текущий уровень - битва с боссом, то при его уничтожении спавнится система
        /// частиц с настроенным в начале уровня цветом
        /// </summary>
        private void SpawnParticles()
        {
            var instance = Instantiate(bossParticleSystem, transform.position, Quaternion.identity);
            var main = instance.main;
            main.startColor = _particleSystemColor;
        }
    }
}