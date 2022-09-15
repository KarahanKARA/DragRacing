using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5.0f;

        private Vector3 _cameraOffset;

        private Transform _playerTransform;
        public Transform PlayerTransform
        {
            get => _playerTransform;
            set => _playerTransform = value;
        }

        private void Start()
        {
            if (PlayerTransform == null)
            {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
            _cameraOffset = transform.position - PlayerTransform.position;
            
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                _cameraOffset = camTurnAngle * _cameraOffset;
            }

            transform.position = PlayerTransform.position + _cameraOffset;
            transform.LookAt(PlayerTransform);
        }
    }
}