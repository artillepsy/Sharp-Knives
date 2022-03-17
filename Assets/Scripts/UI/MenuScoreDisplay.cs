﻿using Core;
using SaveSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MenuScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCount;
        [SerializeField] private TextMeshProUGUI highScore;
        private int _appleCount = 0;
        private void OnEnable() => Events.OnBuy.AddListener(ChangeAppleCount);
        private void Start()
        {
            var manager = FindObjectOfType<SaveManager>();
            _appleCount = manager.Score.AppleCount;
            appleCount.text = manager.Score.AppleCount.ToString();
            highScore.text = manager.Score.HighScore.ToString();
        }
        private void ChangeAppleCount(int cost)
        {
            _appleCount -= cost;
            appleCount.text = _appleCount.ToString();
        }
    }
}