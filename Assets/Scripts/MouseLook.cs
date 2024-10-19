using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private RotationAxes _rotationAxes;
    [SerializeField] private float _sensitivity = 10f;
    [SerializeField] private float _rotationAngle = 45f;

    [SerializeField] private FixedJoystick _joystickRotation;

    private float _rotationX;

    private float _x = 0;
    private float _y = 0;

    private void Update()
    {
        if (Application.isMobilePlatform && _joystickRotation != null)
        {
            _x = _joystickRotation.Horizontal;
            _y = _joystickRotation.Vertical;
        }
        else
        {
            _x = Input.GetAxis("Mouse X");
            _y = Input.GetAxis("Mouse Y");
        }

        Rotate(_x, _y);
    }

    private void Rotate(float x, float y)
    {
        switch (_rotationAxes)
        {
            case RotationAxes.MouseX:
                transform.Rotate(0, x * _sensitivity, 0);
                break;
            case RotationAxes.MouseY:
                _rotationX -= y * _sensitivity;
                _rotationX = Mathf.Clamp(_rotationX, -_rotationAngle, _rotationAngle);
                transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0);
                break;
        }
    }
}