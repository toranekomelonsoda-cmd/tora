using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const float MOVE_SPEED = 5.0f;

    [SerializeField] private Rigidbody rigidbody;

    private PlayerinputActions InputAtions;
    private Vector2 moveInput;
   
    

    private Vector3Å@moveDirection = Vector3.zero;

    public Vector3 CurrentVelocity{ get; private set; }

    private void Awake()
    {
        InputAtions = new PlayerinputActions();
        InputAtions.Player.fire.performed += OnFire ;
    }


     private void OnEnable ()
    {
        InputAtions.Enable();
    }

     private void OnDisable ()
    {
        InputAtions.Disable();
    }


     private void Update()
    {
      moveInput = InputAtions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (rigidbody == null)
        {
            Debug.LogError("RigidbodyÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            return;
        }

        if (moveInput == Vector2.zero)
        {
            rigidbody.linearVelocity = new Vector3(0f, rigidbody.linearVelocity.y, 0f);
            CurrentVelocity = Vector3.zero;
            return;
        }

        Vector3 targetVelocity = new Vector3 (moveInput.x,rigidbody.linearVelocity.y,moveInput.y);
        targetVelocity.Normalize();

        rigidbody.linearVelocity = targetVelocity * MOVE_SPEED;

        CurrentVelocity = rigidbody.linearVelocity;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
    
        Debug.Log("Fire");
    }

    }

