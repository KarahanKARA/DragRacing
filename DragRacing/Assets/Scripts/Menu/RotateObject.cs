using UnityEngine;

namespace Menu
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;
        private bool _canRotateTheCar = false;
        private bool _isMouseOver = false;
        void Update()
        {
            if (_canRotateTheCar)
            {
                if (Input.GetMouseButton(0))
                {
                    Quaternion camTurnAngle =
                        Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeed, Vector3.down);
                    transform.rotation = camTurnAngle * transform.rotation;
                }
                else
                {
                    transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
                }
            }
            else
            {
                transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
            }

            if (!Input.GetMouseButton(0) && !_isMouseOver)
            {
                _canRotateTheCar = false;
            }
        }

        private void OnMouseOver()
        {
            _isMouseOver = true;
            if (!Input.GetMouseButton(0))
            {
                _canRotateTheCar = true;
            }
        }

        private void OnMouseExit()
        {
            _isMouseOver = false;
        }
    }
}