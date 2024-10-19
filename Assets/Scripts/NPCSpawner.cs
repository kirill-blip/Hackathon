using System.Collections.Generic;

using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private int _count = 4;
    [SerializeField] private NonPlayerCharacter _nonPlayerCharacter;
    [SerializeField] private List<Transform> _positions;

    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            SpawnNPC();
        }
    }

    private void SpawnNPC()
    {
        if (_positions.Count <= 0)
        {
            return;
        }

        Vector3 position = _positions[Random.Range(0, _positions.Count)].position;
        NonPlayerCharacter npc = Instantiate(_nonPlayerCharacter, position, Quaternion.identity);
        npc.CollidedWithCar += OnCollidedWithCarHandler;
    }

    private void OnCollidedWithCarHandler(object sender, NonPlayerCharacter npc)
    {
        SpawnNPC();

        Broadcast.Send(new NPCDestoroyed(npc.name));
        npc.CollidedWithCar -= OnCollidedWithCarHandler;
        Destroy(npc.gameObject);
    }
}