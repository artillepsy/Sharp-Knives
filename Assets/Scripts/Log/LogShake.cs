using Core;
using UnityEngine;

namespace Log
{
    public class LogShake : MonoBehaviour
    {
        private Animation _shakeAnim;
        private Material _material;
        private readonly string _amount = "_Amount";
        [Header("Material settings")] 
        [SerializeField] private AnimationCurve _animationCurve;
        [Range(0, 1)]
        [SerializeField] private float maxIntensity = 1f;
        [SerializeField] private float duration = 0.1f;
        private float _totalTime;
        

        private void Awake()
        {
            _totalTime = duration;
            _material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
            _shakeAnim = GetComponent<Animation>();
        }

        private void OnEnable()
        {
            Events.OnKnifeHit.AddListener(() =>
            {
             _shakeAnim.Play();
             _totalTime = 0;
            });
        }

        private void Update()
        {
            if(ShouldShake()) ChangeColor();
        }

        private void ChangeColor()
        {
            var value = _animationCurve.Evaluate(_totalTime / duration);
            value *= maxIntensity;
            Debug.Log(_material.GetFloat(_amount));
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