﻿using System.Collections.Generic;
using Management;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    /// <summary>
    /// Класс, отображающий текущую стадию во время игры
    /// </summary>
    public class StagesDisplay : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private Transform content;
        [SerializeField] private TextMeshProUGUI stageText;
        [SerializeField] private Image imagePrefab;
        [SerializeField] private Image bossImagePrefab;
        [SerializeField] private Color selectedColor;
        /// <summary>
        /// Метод спавнит иконки стадий в окне игры и закрашивает их в цвета
        /// в зависимости от того, какая сейчас стадия
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            var levelManager = FindObjectOfType<LevelManager>();
            var images = new List<Image>();
            for (int i = 0; i < levelManager.StagesInCycle-1; i++)
            {
                images.Add(Instantiate(imagePrefab, content));
            }
            images.Add(Instantiate(bossImagePrefab, content));
            var stages = levelManager.CurrentStage;
            if (stages == 0 && levelManager.WinCount != 0)
            {
                stages = levelManager.StagesInCycle;
            }
            for (int i = 0; i < stages; i++)
            {
                images[i].color = selectedColor;
            }
            stageText.text = "Stage " + (levelManager.WinCount + 1);
        }
    }
}