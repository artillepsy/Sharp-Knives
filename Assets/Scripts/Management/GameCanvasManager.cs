﻿using System.Linq;
using Core;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class GameCanvasManager : AbstractCanvasManager
    {
        public void OnClickPause()
        {
            Time.timeScale = 0f;
            NotifyAll(CanvasType.Pause);
        }

        public void OnClickRestart()
        {
            //NotifyAll(CanvasType.Game);
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnClickResume()
        {
            Time.timeScale = 1f;
            NotifyAll(CanvasType.Game);
        }

        public void OnClickMainMenu()
        {
            FindObjectOfType<SaveLoadManager>().SaveProgress();
            Time.timeScale = 1f;
            SceneManager.LoadSceneAsync("Menu");
        }
        
        private void OnEnable()
        {
            _subs = FindObjectsOfType<MonoBehaviour>().OfType<IOnCanvasChange>().ToList();
            Events.OnFailGame.AddListener( () => NotifyAll(CanvasType.Fail));
        }
        private void Start() => NotifyAll(CanvasType.Game);
    }
}