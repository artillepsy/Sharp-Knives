using System.Linq;
using LevelSettings;
using UnityEngine;

namespace Scriptable
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField] private Level level;

        private void Start()
        {
            var subscribers = FindObjectsOfType<MonoBehaviour>().OfType<IOnLevelLoad>();
            foreach (var subscriber in subscribers)
            {
                subscriber.OnLevelLoad(level);
            }
        }
    }
}