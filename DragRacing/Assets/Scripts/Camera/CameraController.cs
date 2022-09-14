using UnityEngine;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private float rotationSpeed = 5.0f;
        
        private Vector3 _cameraOffset;

        private void Start()
        {
            _cameraOffset = transform.position - playerTransform.position;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
                _cameraOffset = camTurnAngle * _cameraOffset;
            }

            transform.position = playerTransform.position + _cameraOffset;
            transform.LookAt(playerTransform);
        }
    }
}