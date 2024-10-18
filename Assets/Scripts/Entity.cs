using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _range = 25;

    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.SetDestination(GetRandomPosition(_range));
    }

    private void Update()
    {
        if (!_agent.hasPath || _agent.remainingDistance < 0.1f)
        {
            _agent.SetDestination(GetRandomPosition(10));
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected");
            }
        }
    }

    private Vector3 GetRandomPosition(float range)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, 1);

        return hit.position;
    }
}
