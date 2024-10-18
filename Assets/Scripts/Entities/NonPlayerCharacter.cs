using UnityEngine;
using UnityEngine.AI;

public class NonPlayerCharacter : Entity, IInteractable
{
    [SerializeField] private float _velocity = 5;
    [SerializeField] private float _multiplier = 1.5f;

    [Space(5f)]
    [SerializeField] private float _range = 25;
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        if (!_agent.hasPath || _agent.remainingDistance < 0.1f)
        {
            MoveWithRandomPosition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log("Car detected");
            _velocity = _velocity * _multiplier;

            Vector3 directionAwayFromCar = transform.position - other.transform.position;
            Vector3 newPosition = transform.position + directionAwayFromCar.normalized * _range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(newPosition, out hit, _range, NavMesh.AllAreas))
            {
                _agent.SetDestination(hit.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            Debug.Log("Car exited");
            _velocity = _velocity / _multiplier;
        }
    }

    private void MoveWithRandomPosition()
    {
        _agent.speed = _velocity;
        _agent.SetDestination(GetRandomPosition(_range));
    }

    private Vector3 GetRandomPosition(float range)
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, range, 1);

        return hit.position;
    }

    public void Interact()
    {
        Debug.LogError("Method douesn't implented");
    }
}
