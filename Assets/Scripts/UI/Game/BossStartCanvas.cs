using LevelSettings;
using Scriptable;
using TMPro;
using UnityEngine;

namespace Game
{
    public class BossStartCanvas : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private TextMeshProUGUI bossName;
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Custom.IsBoss) return;
            bossName.text = level.Log.Custom.Boss.Name;
        }
    }
}