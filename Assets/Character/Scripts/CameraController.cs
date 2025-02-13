using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float sensitivity = 1;
    [SerializeField, Range(2,10)] float distance; 


    InputAction lookAction;
    Vector2 lookInput = Vector2.zero;
    Vector3 rotation = Vector3.zero; //rotation.x is pitch, y is yaw, z is roll

    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        lookAction.performed += Look;
        lookAction.canceled += Look;

        Quaternion qRotation = Quaternion.LookRotation(target.position - transform.position);
        rotation.x = qRotation.eulerAngles.x;
        rotation.y = qRotation.eulerAngles.y;

    }

    void Update()
    {
        rotation.x += lookInput.y * sensitivity;
        rotation.y += lookInput.x * sensitivity;

        rotation.x = Mathf.Clamp(rotation.x, 20, 89);

        Quaternion qRotation = quaternion.Euler(rotation);
        transform.position = target.position + qRotation * (Vector3.back * distance);
        transform.rotation = qRotation;
    }

    void Look(InputAction.CallbackContext callbackContext)
    {
        lookInput = callbackContext.ReadValue<Vector2>();
    }
}
