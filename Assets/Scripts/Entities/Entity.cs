using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    private protected NavMeshAgent _agent;

    private void Awake()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }
}
