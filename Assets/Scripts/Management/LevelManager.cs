using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Менеджер уровней. Определяет, какой уровень из списка будет текущим
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level> levels;
        [SerializeField] private int stagesInCycle = 5;
        public int CurrentStage => (SaveManager.Inst.Score.WinCount + 1) % stagesInCycle;
        public int StagesInCycle => stagesInCycle;
        public int WinCount => SaveManager.Inst.Score.WinCount;
        private void Start()
        {
            var isBoss = (CurrentStage == 0);
            var level = isBoss ? GetBossLevel() : GetUsualLevel();
            var subscribers = FindObjectsOfType<MonoBehaviour>().OfType<IOnLevelLoad>();
            foreach (var subscriber in subscribers)
            {
                subscriber.OnLevelLoad(level);
            }
        }
        /// <summary>
        /// Метод, который возвращает обычный случайный уровень из списка. В учёт
        /// также идёт текущая стадия игрока и минимальное/максимальное
        /// значение стадий для уровней
        /// </summary>
        private Level GetUsualLevel()
        {
            var usualLevels = new List<Level>();
            var winCount = SaveManager.Inst.Score.WinCount;
            foreach (var level in levels)
            {
                if(level.Log.Settings.IsBoss) continue;
                if (winCount > level.Log.Settings.Default.MaxWinCount ||
                    winCount < level.Log.Settings.Default.MinWinCount) continue;
                
                usualLevels.Add(level);
            }
            if (usualLevels.Count != 0) return usualLevels[Random.Range(0, usualLevels.Count)];
            foreach (var level in levels)
            {
                if(level.Log.Settings.IsBoss) continue;
                usualLevels.Add(level);
            }
            return usualLevels[Random.Range(0, usualLevels.Count)];
        }
        /// <summary>
        /// Метод, который возвращает уровень с боссом из списка доступных
        /// Учитывается шанс выпадения босса
        /// </summary>
        private Level GetBossLevel()
        {
            var bossLevels = new List<Level>();
            foreach (var level in levels)
            {
                if(!level.Log.Settings.IsBoss) continue;
                bossLevels.Add(level);
            }
            var totalValue = 0f;
            bossLevels.ForEach(level => totalValue+=level.Log.Settings.Boss.Chance);
            var randomValue = Random.value * totalValue;
            foreach (var level in bossLevels)
            {
                if (randomValue < level.Log.Settings.Boss.Chance) return level;
                randomValue -= level.Log.Settings.Boss.Chance;
            }
            return bossLevels[bossLevels.Count - 1];
        }
    }
}