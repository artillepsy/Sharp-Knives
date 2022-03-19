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
        //private void OnEnable() => _animation.Play(_appearAnimClip);
        public void PlayAnimation(bool canvasEnabled)
        {
            Debug.Log("anim");
            _animation.Play(canvasEnabled ? _appearAnimClip : _disappearAnimClip);
        }
    }
}