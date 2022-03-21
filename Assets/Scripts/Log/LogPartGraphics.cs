using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Settings.IsBoss) return;
            var materials = GetComponent<MeshRenderer>().sharedMaterials;
            materials[0].mainTexture = level.Log.Settings.LogTexture;
            materials[1].color = level.Log.Settings.Default.InsideColor;
        }
    }
}