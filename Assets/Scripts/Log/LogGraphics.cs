using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogGraphics : MonoBehaviour, IOnLevelLoad
    {
        private Material _material;

        public void OnLevelLoad(Level level)
        {
            _material.mainTexture = level.LogGraphics.LogTexture;
        }
        
        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().sharedMaterial;
        }

       
    }
}