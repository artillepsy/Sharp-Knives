using Core;
using UnityEngine;

namespace Log
{
    /// <summary>
    /// Класс, отвечающий за контроль анимаций бревна
    /// </summary>
    public class LogAnimationController : MonoBehaviour
    {
        [Header("Material settings")] 
        [SerializeField] private AnimationCurve animationCurve;
        [Range(0, 1)]
        [SerializeField] private float maxIntensity = 1f;
        [SerializeField] private float duration = 0.1f;
        private Animation _animation;
        private Material _material;
        private readonly string _amount = "_Amount";
        private readonly string _shakeAnim = "LogShake";
        private float _totalTime;

        private void Awake()
        {
            _totalTime = duration;
            _material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
            _material.SetFloat(_amount, 0);
            _animation = GetComponent<Animation>();
        }
        /// <summary>
        /// ПОдписка на событие втыкание ножа в бревно: Проигрывание анимации "подброса"
        /// и установка таймера в начало
        /// </summary>
        private void OnEnable()
        {
            Events.OnKnifeHit.AddListener(() =>
            {
             _animation.Play(_shakeAnim);
             _totalTime = 0;
            });
        }

        private void Update()
        {
            if(ShouldChangeColor()) ChangeColor();
        }
        /// <summary>
        /// Метод, изменяющий цвет материала бревна при помощи
        /// анимационной кривой
        /// </summary>
        private void ChangeColor()
        {
            var value = animationCurve.Evaluate(_totalTime / duration);
            value *= maxIntensity;
            _material.SetFloat(_amount, value);
        }
        /// <summary>
        /// Проверка на то, должен ли меняться цвет бревна
        /// </summary>
        private bool ShouldChangeColor()
        {
            if (_totalTime >= duration) return false;
            _totalTime += Time.deltaTime;
            return true;
        }
    }
}