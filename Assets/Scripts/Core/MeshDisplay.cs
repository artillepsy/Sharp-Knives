using UnityEngine;

namespace Core
{
    public class MeshDisplay : MonoBehaviour
    {
        [SerializeField] private MeshData meshData;
        
        private void Start()
        {
            GetComponent<MeshFilter>().mesh = meshData.mesh;
        }
    }
}