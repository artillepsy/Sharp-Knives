using Core;
using Knife;
using Log;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class LevelManager : MonoBehaviour
    {
        private int _knifeCount;
        public void SetKnifeCount(int count)
        {
            _knifeCount = count;
        }

        private void OnEnable()
        {
            KnifeStateController.OnDrop.AddListener(EndLevel);
            LogStickness.OnKnifeStick.AddListener(ReduceKnifeCount);
        }

        private void ReduceKnifeCount()
        {
            _knifeCount--;
            if(_knifeCount == 0) EndLevel();
        }

        private void EndLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}