using UnityEngine;

namespace Player
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float topSpeed;
        [SerializeField] private int numberOfGears;

        [Space(20)] [Header("Acceleration is directly proportional to engine power.")] [Range(70, 150)] [SerializeField]
        private int enginePower;

        private float _speed = 0f;

        private int _currentGearTier = 1;

        public int CurrentGearTier
        {
            get => _currentGearTier;
            set => _currentGearTier = value;
        }

        private float _currentRpm;

        public float CurrentRpm
        {
            get => _currentRpm;
            set => _currentRpm = value;
        }

        private void Awake()
        {
            CurrentRpm = 0;
        }

        void Update()
        {
            CurrentRpm = (GetMaxRpm() * _speed) / topSpeed;
            CurrentGearTier = (int)(CurrentRpm / 1000)+1;
            if (Input.GetKey(KeyCode.W))
            {
                _speed += Time.deltaTime * enginePower / 10f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _speed -= Time.deltaTime * enginePower / 5f;
            }
            else
            {
                _speed -= Time.deltaTime * enginePower / 16f;
            }

            if (_speed < 0f)
            {
                _speed = 0f;
            }

            if (_speed > topSpeed)
            {
                _speed = topSpeed;
            }

            transform.position += new Vector3(0, 0, _speed * Time.deltaTime);
        }

        public float GetTopSpeed()
        {
            return topSpeed;
        }

        public float GetCurrentSpeed()
        {
            return _speed;
        }

        public float GetMaxRpm()
        {
            return numberOfGears * 1000 + 500;
        }
    }
}