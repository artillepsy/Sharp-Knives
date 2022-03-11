using Log;
using UnityEngine;

namespace Knife
{
    public class KnifeStateController : MonoBehaviour
    {
        private MovementState _state;

        private void Start()
        {
            _state = MovementState.Ready;
        }
    }
}