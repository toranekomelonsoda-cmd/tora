using Core.Interface;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



namespace TPSRoguelite.InGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float MOVE_SPEED = 5.0f;
        private const float ROTATION_SPEED = 10.0f;
        private const float LASER_MAX_DISTANCE = 50.0f;

        private const int ATTACK_DANGE = 20;

        private const float ATTACK_RANGE = 50f;

        private const int MAX_AMMD = 30;

         



        [SerializeField] private Rigidbody rigidbody;

        private PlayerinputActions InputAtions;
        private Vector2 moveInput;

        private Transform mainCameraTransform;

        [SerializeField] private LineRenderer laserLineRenderer;

        [SerializeField] private Transform weaponOrigin;

        private bool isReloading;

        private Vector3 moveDirection = Vector3.zero;

        public Vector3 CurrentVelocity { get; private set; }

        public int CurrentAmmo { get; private set; }

        

        private void Update()
        {
            moveInput = InputAtions.Player.Move.ReadValue<Vector2>();
            DrawLaserPointer();
        }

        private void DrawLaserPointer()
     {
         if (laserLineRenderer == null || weaponOrigin == null || mainCameraTransform == null)
         {
            return;
         }
 
         laserLineRenderer.SetPosition(0, weaponOrigin.position);

         // カメラの中央から真っ直ぐ前へ光線を飛ばす
         Ray ray = new Ray(mainCameraTransform.position, mainCameraTransform.forward);
 
         // 光線が何かに当たったか判定
        if (Physics.Raycast(ray, out RaycastHit hitInfo, LASER_MAX_DISTANCE))
         {
             laserLineRenderer.SetPosition(1, hitInfo.point);
        }
         else
         {
             // 何も当たらなかったら、最大距離の場所を終点にする    
             laserLineRenderer.SetPosition(1, ray.GetPoint(LASER_MAX_DISTANCE));
         }
     }

        private void Awake()
        {
            if (rigidbody == null)
            {
                Debug.LogError("Rigidbody������܂���I");
            }
            InputAtions = new PlayerinputActions();
            InputAtions.Player.fire.performed += OnFire;

            if (UnityEngine.Camera.main != null)
            {
                mainCameraTransform = UnityEngine.Camera.main.transform;
            }
            else
            {
                Debug.LogError("Main Camera��������܂���I");
            }
        }

         
        
        



         private void OnEnable()
        {
            InputAtions.Enable();
        }

        private void OnDisable()
        {
            InputAtions.Disable();
        }


       

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (rigidbody == null)
            {
                Debug.LogError("Rigidbody���ݒ肳��Ă��܂���");
                return;
            }

            if (moveInput == Vector2.zero)
            {
                rigidbody.linearVelocity = new Vector3(0f, rigidbody.linearVelocity.y, 0f);
                CurrentVelocity = Vector3.zero;
                return;
            }

           

            // �J������̌v�Z�ɕύX
            Vector3 cameraForward = mainCameraTransform.forward;
            Vector3 cameraRight = mainCameraTransform.right;
            
            
            cameraForward.y = 0f;
            cameraRight.y = 0f;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;
            
                     // �L�����N�^�[��i�s�����֊��炩�ɐU���������
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, targetRotation, ROTATION_SPEED * Time.fixedDeltaTime);
            
                     // Y���̑��x�i�����Ȃǁj�͌��݂̕������Z�̒l���ێ����AX��Z�̂ݏ㏑������
            Vector3 targetVelocity = moveDirection * MOVE_SPEED;
            rigidbody.linearVelocity = new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.z);

            CurrentVelocity = rigidbody.linearVelocity;
        }

        private void OnFire(InputAction.CallbackContext context)
        {

            Ray ray = new Ray(mainCameraTransform.position, mainCameraTransform.forward);
            if (Physics.Raycast(ray,out RaycastHit hitlnfo, ATTACK_RANGE))
            {
                Debug.Log($"[hitinfo.collider.name]に命中！");
                IDamageable target = hitlnfo.collider.GetComponent<IDamageable>();
            }
        }

    }
}
