using UnityEngine;

public class NPCSpawner : Spawner
{
    private protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < _count; i++)
        {
            Spawn();
        }
    }

    private protected override void Spawn()
    {
        if (_positions.Count <= 0)
        {
            return;
        }

        Vector3 position = _positions[Random.Range(0, _positions.Count)].position;

        NonPlayerCharacter prefab = _prefabs[Random.Range(0, _prefabs.Count)].GetComponent<NonPlayerCharacter>();

        NonPlayerCharacter npc = Instantiate(prefab, position, Quaternion.identity);
        npc.CollidedWithCar += OnCollidedWithCarHandler;
    }

    private void OnCollidedWithCarHandler(object sender, NonPlayerCharacter npc)
    {
        Spawn();

        Broadcast.Send(new NPCDestoroyedMessage(npc.name));
        npc.CollidedWithCar -= OnCollidedWithCarHandler;
        Destroy(npc.gameObject);
    }
}