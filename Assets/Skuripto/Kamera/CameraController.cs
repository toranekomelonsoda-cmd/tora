using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TPSRoguelite.InGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        
        private const float LOOK_SENSITIVITY = 0.2f;

        private const float DISTANCE = 5.0f;

        private const float HEIGHT_OFFSET = 1.5f;

        private const float MIN_PITCH = -10f;

        private const float MAX_PITCH = 60f;

        
        [SerializeField] private Transform target;

       
        private PlayerinputActions inputActions;

        private Vector2 lookInput = Vector2.zero;

        
        private float currentYaw = 0f;

       
        private float currentPitch = 20f;

        private void Awake()
        {
            inputActions = new PlayerinputActions();

           
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        private void Update()
        {
           
            lookInput = inputActions.Player.Look.ReadValue<Vector2>();

            currentYaw += lookInput.x * LOOK_SENSITIVITY;
            currentPitch -= lookInput.y * LOOK_SENSITIVITY;


            currentPitch = Mathf.Clamp(currentPitch, MIN_PITCH, MAX_PITCH);
        }

        private void LateUpdate()
        {
           
            if (target == null)
            {
                return;
            }

            Vector3 targetPosition = target.position + Vector3.up * HEIGHT_OFFSET;

            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0f);

            Vector3 cameraPosition = targetPosition - (rotation * Vector3.forward * DISTANCE);

            

            transform.position = cameraPosition;
            transform.rotation = rotation;
        }
    }
    //Quoternion rotation = Quaternino.Euler(currentPitch,currentYaw,Of);
    //Vector3 camerPosition = targetPosition - (rotation * Vecter3. forward * DISTANCE)

}