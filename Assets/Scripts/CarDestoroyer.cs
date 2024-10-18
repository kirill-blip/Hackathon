using System;

using UnityEngine;

public class CarDestoroyer : MonoBehaviour
{
    public event EventHandler<GameObject> CarDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car _))
        {
            CarDestroyed?.Invoke(this, other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
