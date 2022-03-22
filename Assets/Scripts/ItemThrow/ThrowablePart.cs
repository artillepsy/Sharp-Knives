using UnityEngine;

namespace ItemThrow
{
    /// <summary>
    /// Класс, отвечающий за поведение игрового объекта в случае броска
    /// </summary>
    public class ThrowablePart : MonoBehaviour
    {
        [SerializeField] protected float itemMass = 2f;
        [SerializeField] private bool threeDimensions;
        [Space]
        [SerializeField] private float minImpulse = 15;
        [SerializeField] private float maxImpulse = 25f; 
        [Space]
        [SerializeField] private float minTorque = 3f;
        [SerializeField] private float maxTorque = 6f;
        [Space]
        [SerializeField] protected float minDeviationAngle = -45f;
        [SerializeField] protected float maxDeviationAngle = 45f;
        
        private float _impulse => Random.Range(minImpulse,maxImpulse);
        private float _torque => Random.Range(minTorque, maxTorque);
        private float _deviation => Random.Range(minDeviationAngle, maxDeviationAngle);
        private Quaternion _rotation => Quaternion.Euler(0, 0, _deviation);

        /// <summary>
        /// Данный метод изменяет ограничения на Rigidbody в зависимости от того, спрайт это или 3D объект (Части бревна)
        /// В случае спрайта, дополнительно идёт заморозка на вращение по двум осям
        /// </summary>
        private RigidbodyConstraints GetConstraints()
        {
            if (!threeDimensions)
            {
                return RigidbodyConstraints.FreezeRotationX | 
                       RigidbodyConstraints.FreezeRotationY | 
                       RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                return RigidbodyConstraints.FreezePositionZ;
            }
        }
        /// <summary>
        /// Данный метод возвращает ось и направление вращения объекта
        /// </summary>
        private Vector3 GetAxis()
        {
            if (threeDimensions) return Random.rotation * Vector3.forward;
            return Random.value > 0.5f ? Vector3.forward : -Vector3.forward;
        }
        /// <summary>
        /// Данный метод отвечает за настройку Rigidbody и инициализацией его полей
        /// значениями из инспектора непосредственно перед броском
        /// </summary>
        public void Throw(Vector3 direction)
        {
            var rb = GetComponent<Rigidbody>();
            rb.transform.SetParent(null);
            rb.mass = itemMass;
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.constraints = GetConstraints();
            direction = _rotation * direction.normalized;
            rb.AddForce(direction * _impulse, ForceMode.Impulse);
            rb.AddTorque(GetAxis() * _torque, ForceMode.Impulse);
        }
    }
}