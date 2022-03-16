using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            if (!level.GraphicsData.IsBoss) return;
            var materials = GetComponent<MeshRenderer>().sharedMaterials;
            materials[0].mainTexture = level.GraphicsData.LogTexture;
            materials[1].color = level.GraphicsData.PartsInsideColor;
        }
    }
}