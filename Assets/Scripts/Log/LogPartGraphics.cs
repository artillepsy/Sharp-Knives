using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Custom.IsBoss) return;
            var materials = GetComponent<MeshRenderer>().sharedMaterials;
            materials[0].mainTexture = level.Log.Custom.LogTexture;
            materials[1].color = level.Log.Custom.Default.InsideColor;
        }
    }
}