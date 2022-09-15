using UnityEngine;

namespace Player
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float topSpeed;
        [SerializeField] private int numberOfGears;
        
        [Range(70,150)] 
        [SerializeField] private int motorPower;

        private float _speed = 0f;
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _speed += Time.deltaTime*motorPower/10f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _speed -= Time.deltaTime*motorPower/5f;
            }
            else
            {
                _speed -= Time.deltaTime*motorPower/20f;
            }
            
            if (_speed <0f)
            {
                _speed = 0f;
            }
            if (_speed > topSpeed)
            {
                _speed = topSpeed;
            }
            transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
        }
    }
}
