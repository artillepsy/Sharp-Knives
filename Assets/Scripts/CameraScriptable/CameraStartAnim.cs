
using UnityEngine;

namespace CameraScriptable
{
    /// <summary>
    /// Класс, отвечающий за анимацию покачивания в начале игрового уровня
    /// </summary>
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
        /// <summary>
        /// Приближение камеры в начале уровня при помощи выстроенной анимационной криой
        /// </summary>
        private void Play() =>  _camera.orthographicSize = animationCurve.Evaluate(_currentTime / animTime);
        /// <summary>
        /// Проверка на то, должно ли проигрываться приближение камеры
        /// </summary>
        /// <returns></returns>
        private bool ShouldPlay()
        {
            if (_currentTime >= animTime) return false;
            _currentTime += Time.deltaTime;
            return true;
        }
    }
}