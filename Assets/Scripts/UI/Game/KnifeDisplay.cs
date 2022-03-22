using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    /// <summary>
    /// Метод отображения на панели HUD запускаемых ножей во время игры
    /// </summary>
    public class KnifeDisplay : MonoBehaviour
    {
        [SerializeField] private Transform contentPane;
        [SerializeField] private Image knifeImage;
        [SerializeField] private Image emptyKnife;
        private List<Image> _knifeImageList;
        private int _knifeIndex = 0;
        
        public void SetKnifeCount(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var instance = Instantiate(knifeImage, contentPane);
                _knifeImageList.Add(instance);
            }
        }

        private void ReduceKnifeCount()
        {
            if (_knifeIndex > _knifeImageList.Count - 1) return;
            var knife = _knifeImageList[_knifeIndex];
            knife.sprite = emptyKnife.sprite;
            knife.color = emptyKnife.color;
            _knifeIndex++;
        }

        private void Awake()
        {
            _knifeImageList = new List<Image>();
        }

        private void OnEnable()
        {
            Events.OnThrow.AddListener(ReduceKnifeCount);
        }
    }
}
