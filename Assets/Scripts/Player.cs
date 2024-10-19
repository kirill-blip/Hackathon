using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private FixedJoystick _joystickMovement;
    [SerializeField] private FixedJoystick _joystickRotation;

    private float _horizontalInput = 0;
    private float _verticalInput = 0;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rigidbody = GetComponent<Rigidbody>();

        Broadcast.Subscribe<InteractMessage>(OnInteractHandler);
    }

    private void OnDestroy()
    {
        Broadcast.Unsubscribe<InteractMessage>(OnInteractHandler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        Move();
    }

    private void Move()
    {
        if (Application.isMobilePlatform && _joystickMovement != null)
        {
            _horizontalInput = _joystickMovement.Horizontal;
            _verticalInput = _joystickMovement.Vertical;
        }
        else
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
        }

        Vector3 direction = transform.TransformDirection(_horizontalInput, 0, _verticalInput);
        Vector3 velocity = direction * _speed;

        _rigidbody.velocity = new Vector3(velocity.x, _rigidbody.velocity.y, velocity.z);
    }

    public void Interact()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void OnInteractHandler(InteractMessage message)
    {
        Interact();
    }
}
