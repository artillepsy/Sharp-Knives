using UnityEngine;

namespace Log
{
    public class ItemPlacement : MonoBehaviour
    {
        private CapsuleCollider _parentCollider;
        private float _prevColliderRadius = -1f;
        private Vector3 _direction;
        private Vector3 _center;
        private void Start()
        {
            _center = transform.parent.position;
            _direction = (transform.position - _center).normalized;
            _parentCollider = GetComponentInParent<CapsuleCollider>();
        }

        private void Update()
        {
            if (_parentCollider.radius == _prevColliderRadius) return;
            _prevColliderRadius = _parentCollider.radius;
            transform.position = _center + _direction * _prevColliderRadius;
            transform.up = _direction;
        }
    }
}