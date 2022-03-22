using UnityEngine;

namespace UI
{
    /// <summary>
    /// Класс, контролирующий анимации у UI элементов
    /// </summary>
    public class UIAnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationClip appearAnimClip;
        [SerializeField] private AnimationClip disappearAnimClip;
        private Animation _animation;
        private Canvas _canvas;
        private void Awake() => _animation = GetComponent<Animation>();
        public void PlayAnimation(bool canvasEnabled)
        {
            _animation.Play(canvasEnabled ? appearAnimClip.name : disappearAnimClip.name);
        }
    }
}