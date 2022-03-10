using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "New Knife", menuName = "Knife")]
    public class MeshData : ScriptableObject
    {
        public Mesh mesh;
    }
}
