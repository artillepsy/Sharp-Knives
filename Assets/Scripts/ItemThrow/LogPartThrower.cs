using System.Collections.Generic;
using System.Linq;
using Core;
using Log;
using Scriptable;
using UnityEngine;

namespace ItemThrow
{
    /// <summary>
    /// Класс, отвечающий за бросок частей бревна в случае победы
    /// </summary>
    public class LogPartThrower : MonoBehaviour, IOnLevelLoad
    {
        private GameObject _log;
        private List<ThrowablePart> _parts;
        /// <summary>
        /// Здесь идёт проверка на то, что загруженный уровень не является битвой с боссом. Если оно так,
        /// то в скрипте не идёт подписка на события в случае победы
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Settings.IsBoss) return;
            Events.OnWinGame.RemoveListener(ThrowParts);
            Events.OnWinGame.AddListener(() => _log.SetActive(false));
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _parts = GetComponentsInChildren<ThrowablePart>().ToList();
            _parts.ForEach(part => part.gameObject.SetActive(false));
            _log = FindObjectOfType<LogRotation>().gameObject;
            Events.OnWinGame.AddListener(ThrowParts);
        }
        /// <summary>
        /// В данном методе идёт активизация частей бревна и придача им имбульса
        /// </summary>
        private void ThrowParts()
        {
            transform.rotation = _log.transform.rotation;
            _log.SetActive(false);
            _parts.ForEach(part => part.gameObject.SetActive(true));
            _parts.ForEach(part => part.Throw(part.transform.position - _log.transform.position));
        }
    }
}