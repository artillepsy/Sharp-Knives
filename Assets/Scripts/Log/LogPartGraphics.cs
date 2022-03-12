using Level;
using UnityEngine;

namespace Log
{
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        private Material[] _materials;

        public void OnLevelLoad(LevelData levelData)
        {
            _materials[0].mainTexture = levelData.LogTexture;
            _materials[1].color = levelData.InsideLogPartsColor;
        }
        
        private void Awake()
        {
            _materials = GetComponent<MeshRenderer>().sharedMaterials;
        }
    }
}