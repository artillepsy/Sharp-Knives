using System.Linq;
using UnityEngine;

namespace Level
{
    public class LevelSettings : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;

        private void Start()
        {
            var subscribers = FindObjectsOfType<MonoBehaviour>().OfType<IOnLevelLoad>();
            foreach (var subscriber in subscribers)
            {
                subscriber.OnLevelLoad(levelData);
            }
        }
    }
}