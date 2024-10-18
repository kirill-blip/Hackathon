using System;

using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class NonPlayerCharacter : MonoBehaviour, IInteractable
{
    [SerializeField] private float _velocity = 5;
    [SerializeField] private float _multiplier = 1.5f;

    [Space(5f)]
    [SerializeField] private float _range = 25;
    [SerializeField] private LayerMask _layerMask;

    private readonly float _stuckCheckInterval = 0.1f;
    private float _lastStuckCheckTime;
    private bool _isStoppedByCar = false;

    private NavMeshAgent _agent;
    private Collider _currentCarCollider;
    private Vector3 _lastPosition;

    public event EventHandler<NonPlayerCharacter> CollidedWithCar;

    private void Awake()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isStoppedByCar && _currentCarCollider == null)
        {
            _isStoppedByCar = false;
            _agent.isStopped = false;
        }

        if (!_isStoppedByCar && (!_agent.hasPath || _agent.remainingDistance < 0.1f))
        {
            MoveWithRandomPosition();
        }

        CheckIfStuck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_layerMask == (_layerMask | (1 << collision.gameObject.layer)))
        {
            CollidedWithCar?.Invoke(this, this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            _isStoppedByCar = true;
            _agent.isStopped = true;
            _currentCarCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_layerMask == (_layerMask | (1 << other.gameObject.layer)))
        {
            _isStoppedByCar = false;
            _agent.isStopped = false;
            _currentCarCollider = null;
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

    private void CheckIfStuck()
    {
        if (Time.time - _lastStuckCheckTime > _stuckCheckInterval)
        {
            if (Vector3.Distance(transform.position, _lastPosition) < 0.1f)
            {
                Debug.Log("NPC is stuck, finding new position");
                MoveWithRandomPosition();
            }

            _lastPosition = transform.position;
            _lastStuckCheckTime = Time.time;
        }
    }

    public void Interact()
    {
        Debug.LogError("Method doesn't implemented");
    }
}
