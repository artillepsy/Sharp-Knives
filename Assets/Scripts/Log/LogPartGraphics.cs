using Scriptable;
using UnityEngine;

namespace Log
{
    /// <summary>
    /// Класс, отвечающий за инициализацию графики частей бревна
    /// </summary>
    public class LogPartGraphics : MonoBehaviour, IOnLevelLoad
    {
        /// <summary>
        /// Изменение материала частей бревна, если текущий уровень - не битва с боссом
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Settings.IsBoss) return;
            var materials = GetComponent<MeshRenderer>().sharedMaterials;
            materials[0].mainTexture = level.Log.Settings.LogTexture;
            materials[1].color = level.Log.Settings.Default.InsideColor;
        }
    }
}