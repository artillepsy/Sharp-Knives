using Core;
using Scriptable;
using UI.Game;
using UI.Shop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Management
{
    /// <summary>
    /// Класс, отвечающий за запуск ножей во время игры
    /// </summary>
    public class KnifeManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private Animation knifePrefab;
        [SerializeField] private Transform startPosition;
        [SerializeField] private ParticleSystem hitParticleSystem;
        private int _knifeCount;
        private TapInput _tapInput;
        private float _reloadTime;
        private Animation _readyKnife;
        public static ParticleSystem HitParticleSystem;
        private readonly string _appearAnimClip = "Appear";
        private readonly string _disappearAnimClip = "Disappear";

        /// <summary>
        /// Инициализация количества ножей и время перезарядки во время загрузки уровня
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            var main = hitParticleSystem.main;
            main.startColor = level.Log.Settings.HitParticleColor;
            HitParticleSystem = hitParticleSystem;
            
            _knifeCount = Random.Range(level.Knife.MinCount, level.Knife.MaxCount + 1);
            FindObjectOfType<KnifeDisplay>().SetKnifeCount(_knifeCount); 
            FindObjectOfType<GameManager>().SetKnifeCount(_knifeCount); 
            
            _reloadTime = FindObjectOfType<TapInput>().ReloadTimeInSeconds;
            Events.OnKnifeHit.AddListener(OnHit);
            OnHit();
        }

        /// <summary>
        /// Спавн нового ножа и проигрывание начальной анимации при втыкании предыдущего
        /// ножа в бревно
        /// </summary>
        private void OnHit()
        {
            if (_knifeCount == 0) return;
            _readyKnife = Instantiate(knifePrefab, startPosition.position, Quaternion.identity);
            _readyKnife[_appearAnimClip].speed = 1f/_reloadTime;
            _readyKnife.Play(_appearAnimClip);
            _knifeCount--;
        }

       
    }
}
