using System.Collections.Generic;
using LevelSettings;
using Scriptable;
using UnityEngine;

namespace Log
{
    public class ItemSpawner : MonoBehaviour, IOnLevelLoad
    {
        public void OnLevelLoad(Level level)
        {
            var shouldSpawnApples = Random.value < level.AppleSpawnChance;
            var shouldSpawnKnifes = Random.value < level.KnifeSpawnChance;

            if (!shouldSpawnApples && !shouldSpawnKnifes) return;
            
            var spawnPoints = InitSpawnPoints(level.MinItemSpawnDistance);
            if (shouldSpawnApples)
            {
                var appleCount = Random.Range(level.MinSpawnedAppleCount, level.MaxSpawnedAppleCount + 1);
                SpawnItems(level.ApplePrefab, appleCount, spawnPoints);
            }
            if (shouldSpawnKnifes)
            {
                var knifeCount = Random.Range(level.MinSpawnedKnifeCount, level.MaxSpawnedKnifeCount + 1);
                SpawnItems(level.KnifePrefab, knifeCount, spawnPoints);
            }
        }

        private List<Vector3> InitSpawnPoints(float minSpawnDistance)
        {
            var points = new List<Vector3>();
            var logRadius = GetComponent<CapsuleCollider>().radius;
            var center = transform.position;
            var direction = Vector3.up * logRadius;
            var angleStep = Mathf.Rad2Deg * minSpawnDistance / (2 * logRadius);
            var pointCount = (int) (360 / angleStep);

            for (var i = 0; i < pointCount; i++)
            {
                direction = Quaternion.Euler(0, 0, angleStep) * direction;
                points.Add(center + direction);
            }

            return points;
        }

        private void SpawnItems(Transform prefab, int count, List<Vector3> points)
        {
            for (var i = 0; i < count; i++)
            {
                if (points.Count == 0) return;
                var point = points[Random.Range(0, points.Count)];
                points.Remove(point);

                var instance = Instantiate(prefab);
                instance.transform.SetParent(transform);
                instance.position = point;
                instance.up = point - transform.position;
            }
        }
    }
}