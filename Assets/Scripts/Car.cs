using UnityEngine;
using System.Collections;
using Unity.AI.Navigation;

public class Car : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _secondPoint;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        StartCoroutine(MoveBetweenPoints());
    }

    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            yield return MoveToPoint(_firstPoint.position);
            yield return MoveToPoint(_secondPoint.position);
        }
    }

    private IEnumerator MoveToPoint(Vector3 target)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        _navMeshSurface.BuildNavMesh();
    }
}
