using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private protected List<GameObject> _prefabs;
    [SerializeField] private protected int _count;

    [Space(10f)]
    [SerializeField] private Transform _spawnPositionsParent;

    private protected List<Transform> _positions;

    private protected virtual void Awake()
    {
        _positions = new List<Transform>();

        if (_spawnPositionsParent == null)
        {
            return;
        }

        foreach (Transform child in _spawnPositionsParent)
        {
            _positions.Add(child);
        }
    }

    private protected abstract void Spawn();
}
