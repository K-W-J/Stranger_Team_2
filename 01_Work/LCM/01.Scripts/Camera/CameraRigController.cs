using Unity.Cinemachine;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Camera
{
    public class CameraRigController : MonoBehaviour
    {
        [Header("Input")] [SerializeField] private InputSO playerInput;

        [Header("Camera")] [SerializeField] private CinemachinePositionComposer positionComposer;

        [Header("Movement")] [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float scrollSpeed = 5f;
        [Range(0f, 15f)] [SerializeField] private float minZoom = 0.1f;
        [Range(0f, 40f)] [SerializeField] private float maxZoom = 0.1f;

        [SerializeField] private Vector2 movementClampZone;

        private Rigidbody _rigid;
        private bool _isMiddleBtnPressed;

        [Header("Rotation Settings")] [SerializeField]
        private float horizontalSensitivity = 2f;

        [SerializeField] private float verticalSensitivity = 2f;
        [Range(0f, 70f)] [SerializeField] private float minVerticalAngle = -45f;
        [Range(-10f, 90f)] [SerializeField] private float maxVerticalAngle = 45f;

        private float _currentVerticalAngle;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            playerInput.OnCameraMoveEvent += HandleCameraMove;
            playerInput.OnPressedWheelEvent += () => _isMiddleBtnPressed = true;
            playerInput.OnCanceledWheelEvent += () => _isMiddleBtnPressed = false;
            playerInput.OnCameraScrollEvent += HandleCameraScroll;


            _currentVerticalAngle = positionComposer.transform.localEulerAngles.x;
            _currentVerticalAngle = _currentVerticalAngle > 180f ? _currentVerticalAngle - 360f : _currentVerticalAngle;
            _currentVerticalAngle = Mathf.Clamp(
                _currentVerticalAngle,
                minVerticalAngle,
                maxVerticalAngle
            );
        }


        private void OnDestroy()
        {
            playerInput.OnCameraMoveEvent -= HandleCameraMove;
            playerInput.OnPressedWheelEvent -= () => _isMiddleBtnPressed = true;
            playerInput.OnCanceledWheelEvent -= () => _isMiddleBtnPressed = false;
            playerInput.OnCameraScrollEvent += HandleCameraScroll;
        }

        private void Update()
        {
            CameraRotate();
        }

        private void HandleCameraScroll(Vector2 scrollDelta)
        {
            float scrollWheel = scrollDelta.y;
            positionComposer.CameraDistance -= scrollWheel * Time.deltaTime * scrollSpeed;
            positionComposer.CameraDistance = Mathf.Clamp(positionComposer.CameraDistance, minZoom, maxZoom);
        }

        private void HandleCameraMove(Vector2 movement)
        {
            CameraMove(movement);
        }

        private void CameraMove(Vector2 movement)
        {
            Vector3 cameraForward = Vector3.ProjectOnPlane(positionComposer.transform.forward, Vector3.up).normalized;
            Vector3 cameraRight = Vector3.ProjectOnPlane(positionComposer.transform.right, Vector3.up).normalized;
            Vector3 moveDirection = (cameraForward * movement.y + cameraRight * movement.x).normalized;
            _rigid.linearVelocity = moveDirection * moveSpeed;
        }


        private void CameraRotate()
        {
            if (_isMiddleBtnPressed)
            {
                float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
                float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

                positionComposer.transform.Rotate(Vector3.up, mouseX, Space.World);
                _currentVerticalAngle -= mouseY;
                _currentVerticalAngle = Mathf.Clamp(
                    _currentVerticalAngle,
                    minVerticalAngle,
                    maxVerticalAngle
                );

                positionComposer.transform.localRotation = Quaternion.Euler(
                    _currentVerticalAngle,
                    positionComposer.transform.localEulerAngles.y,
                    positionComposer.transform.localEulerAngles.z
                );
            }
        }
    }
}