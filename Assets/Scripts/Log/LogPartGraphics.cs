using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            DefaultGraphics graphics = level.LogGraphics as DefaultGraphics;
            var materials = GetComponent<MeshRenderer>().sharedMaterials;
            materials[0].mainTexture = level.LogGraphics.LogTexture;
            materials[1].color = graphics.PartsInsideColor;
        }
    }
}