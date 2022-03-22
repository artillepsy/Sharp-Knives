using System.Collections.Generic;
using Scriptable;
using UnityEngine;

namespace Log
{
    /// <summary>
    /// Класс, отвечающий за спавн яблок и ножей в начале уровня
    /// </summary>
    public class ItemSpawner : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private Transform applePrefab;
        [SerializeField] private Transform knifePrefab;
        /// <summary>
        /// проверка при загрузке уровня на то, нужно ли спавнить яблоки и ножи. Если нужно,
        /// идёт процедура спавна
        /// </summary>
        public void OnLevelLoad(Level level)
        {
            var shouldSpawnApples = Random.value < level.Items.AppleSpawnChance;
            var shouldSpawnKnifes = Random.value < level.Items.KnifeSpawnChance;

            if (!shouldSpawnApples && !shouldSpawnKnifes) return;
            var coll = GetComponentInChildren<CapsuleCollider>();
            var spawnPoints = InitSpawnPoints(level.Items.MinSpawnDistance, coll);
            if (shouldSpawnApples)
            {
                var appleCount = Random.Range(level.Items.MinAppleCount, level.Items.MaxAppleCount + 1);
                SpawnItems(applePrefab, appleCount, spawnPoints, coll.transform);
            }
            if (shouldSpawnKnifes)
            {
                var knifeCount = Random.Range(level.Items.MinKnifeCount, level.Items.MaxKnifeCount + 1);
                SpawnItems(knifePrefab, knifeCount, spawnPoints, coll.transform);
            }
        }
        /// <summary>
        /// Инициализация доступных точек для спавна объектов. Количество точек зависит от указанного
        /// расстояния в настройках уровня
        /// </summary>
        private List<Vector3> InitSpawnPoints(float minSpawnDistance, CapsuleCollider coll)
        {
            var points = new List<Vector3>();
            var logRadius = coll.radius;
            var center = coll.transform.position;
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
        /// <summary>
        /// Процедура спавна. Точки, на которых заспавнились объекты, удаляются из списка доступных
        /// </summary>
        private void SpawnItems(Transform prefab, int count, List<Vector3> points, Transform parent)
        {
            for (var i = 0; i < count; i++)
            {
                if (points.Count == 0) return;
                var point = points[Random.Range(0, points.Count)];
                points.Remove(point);

                var instance = Instantiate(prefab);
                instance.transform.SetParent(parent);
                instance.position = point;
                instance.up = point - transform.position;
            }
        }
    }
}