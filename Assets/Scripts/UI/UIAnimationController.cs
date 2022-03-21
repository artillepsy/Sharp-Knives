using UnityEngine;

namespace UI
{
    public class UIAnimationController : MonoBehaviour
    {
        [SerializeField] private AnimationClip appearAnimClip;
        [SerializeField] private AnimationClip disappearAnimClip;
        private readonly string _appearClipName = "Appear";
        private readonly string _disappearClipName = "Disappear";
        private Animation _animation;
        private Canvas _canvas;

        private void Awake()
        {
            _animation = GetComponent<Animation>();
            _animation.AddClip(appearAnimClip, _appearClipName);
            _animation.AddClip(disappearAnimClip, _disappearClipName);
        }

        public void PlayAnimation(bool canvasEnabled)
        {
            _animation.Play(canvasEnabled ? _appearClipName : _disappearClipName);
        }
    }
}