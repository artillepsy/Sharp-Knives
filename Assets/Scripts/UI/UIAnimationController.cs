using UnityEngine;

namespace UI
{
    public class UIAnimationController : MonoBehaviour
    {
        private Animation _animation;
        private Canvas _canvas;
        private readonly string _appearAnimClip = "Appear";
        private readonly string _disappearAnimClip = "Disappear";

        private void Awake() => _animation = GetComponent<Animation>();
        public void PlayAnimation(bool canvasEnabled)
        {
           // if (!gameObject.activeSelf) return;
            _animation.Play(canvasEnabled ? _appearAnimClip : _disappearAnimClip);
        }
    }
}