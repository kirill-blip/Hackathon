using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _velocity;

    private void Update()
    {
        transform.Translate(Vector3.forward * _velocity * Time.deltaTime);
    }
}
