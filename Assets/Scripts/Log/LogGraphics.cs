using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogGraphics : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            Debug.Log(level);
            Debug.Log(level.GraphicsData.LogTexture);
            var material = GetComponent<MeshRenderer>().sharedMaterial;
            material.mainTexture = level.GraphicsData.LogTexture;
        }
    }
}