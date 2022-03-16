﻿
using UnityEngine;

namespace CameraScriptable
{
    public class CameraStartAnim : MonoBehaviour
    {
        [SerializeField] private AnimationCurve animationCurve;
        [SerializeField] private float animTime = 1f;
        private Camera _camera;
        private float _currentTime = 0f;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _currentTime = 0f;
        }

        private void Update()
        {
            if (ShouldPlay()) Play();
            else enabled = false;
        }
        private void Play() =>  _camera.orthographicSize = animationCurve.Evaluate(_currentTime / animTime);
        private bool ShouldPlay()
        {
            if (_currentTime >= animTime) return false;
            _currentTime += Time.deltaTime;
            return true;
        }
    }
}