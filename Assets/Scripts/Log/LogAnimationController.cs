using Core;
using UnityEngine;

namespace Log
{
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
            _animation = GetComponent<Animation>();
        }

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
            if(ShouldShake()) ChangeColor();
        }
        private void ChangeColor()
        {
            var value = animationCurve.Evaluate(_totalTime / duration);
            value *= maxIntensity;
            _material.SetFloat(_amount, value);
        }
        private bool ShouldShake()
        {
            if (_totalTime >= duration) return false;
            _totalTime += Time.deltaTime;
            return true;
        }
    }
}