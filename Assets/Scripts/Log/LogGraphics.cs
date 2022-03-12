using Level;
using UnityEngine;

namespace Log
{
    public class LogGraphics : MonoBehaviour, IOnLevelLoad
    {
        private Material _material;

        public void OnLevelLoad(LevelData levelData)
        {
            _material.mainTexture = levelData.LogTexture;
        }
        
        private void Awake()
        {
            _material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        }

       
    }
}