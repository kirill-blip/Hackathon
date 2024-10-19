using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarSpawner : Spawner
{
    [SerializeField] private float _time;

    private Dictionary<Transform, GameObject> _spawnedCars = new Dictionary<Transform, GameObject>();

    private protected override void Awake()
    {
        base.Awake();

        _count = _positions.Count;

        CarDestoroyer[] carDestoroyers = FindObjectsOfType<CarDestoroyer>();

        foreach (CarDestoroyer carDestoroyer in carDestoroyers)
        {
            carDestoroyer.CarDestroyed += OnCarDestroyedHandler;
        }

        StartCoroutine(SpawnCarInTime());
    }

    private IEnumerator SpawnCarInTime()
    {
        for (int i = 0; i < _count; i++)
        {
            Spawn();
            yield return new WaitForSeconds(_time);
        }
    }

    private protected override void Spawn()
    {
        List<Transform> availablePositions = new List<Transform>(_positions);

        foreach (var position in _spawnedCars.Keys)
        {
            availablePositions.Remove(position);
        }

        if (availablePositions.Count == 0)
        {
            Debug.LogWarning("No available positions to spawn a car.");
            return;
        }

        GameObject randomCar = _prefabs[Random.Range(0, _prefabs.Count)];
        Transform randomPosition = availablePositions[Random.Range(0, availablePositions.Count)];

        GameObject spawnedCar = Instantiate(randomCar, randomPosition.position, randomPosition.localRotation);
        _spawnedCars[randomPosition] = spawnedCar;
    }

    private void OnCarDestroyedHandler(object sender, GameObject carGameObject)
    {
        Transform carPosition = null;

        foreach (var kvp in _spawnedCars)
        {
            if (kvp.Value == carGameObject)
            {
                carPosition = kvp.Key;
                break;
            }
        }

        if (carPosition != null)
        {
            _spawnedCars.Remove(carPosition);
        }

        Invoke(nameof(Spawn), _time);
    }
}
