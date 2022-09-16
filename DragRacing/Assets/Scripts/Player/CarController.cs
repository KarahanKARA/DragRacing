using UnityEngine;

namespace Player
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private GameObject carStopLight;
        [SerializeField] private GameObject wheelSmoke;
        [SerializeField] private GameObject exhaustFumes;
        [SerializeField] private float topSpeed;
        [SerializeField] private int numberOfGears;

        [Space(20)] [Header("Acceleration is directly proportional to engine power.")] [Range(70, 150)] [SerializeField]
        private int enginePower;

        private float _speed = 0f;
        private float _tempTime = 0f;

        //-----PROPS-----//
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
            carStopLight.SetActive(false);
            exhaustFumes.SetActive(true);
        }

        void Update()
        {
            Inputs();
            ClampSpeed();
            CurrentRpm = (GetMaxRpm() * _speed) / topSpeed;
            CurrentGearTier = (int)(CurrentRpm / 1000) + 1;
            transform.position += new Vector3(0, 0, _speed * Time.deltaTime / 2);

            if (GameManager.instance.IsPlayerFinished)
            {
                if (_speed > 0f)
                {
                    _speed -= Time.deltaTime * enginePower;
                }
                else
                {
                    _speed = 0f;
                }

                transform.rotation =
                    Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -45f, 0), _tempTime * 0.1f);
                _tempTime += Time.deltaTime;
                wheelSmoke.SetActive(false);
            }
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
            return (numberOfGears - 2) * 1000 + 999;
        }

        private void Inputs()
        {
            if (GameManager.instance.CanPlayerRace)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    _speed += Time.deltaTime * enginePower / 7f;
                    if (_speed <= 30f)
                    {
                        wheelSmoke.SetActive(true);
                    }
                    else
                    {
                        wheelSmoke.SetActive(false);
                    }

                    carStopLight.SetActive(false);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    _speed -= Time.deltaTime * enginePower / 5f;
                    carStopLight.SetActive(true);
                    wheelSmoke.SetActive(false);
                }
                else
                {
                    _speed -= Time.deltaTime * enginePower / 16f;
                    carStopLight.SetActive(false);
                    wheelSmoke.SetActive(false);
                }
            }
        }

        private void ClampSpeed()
        {
            if (_speed < 0f)
            {
                _speed = 0f;
            }

            if (_speed > topSpeed)
            {
                _speed = topSpeed;
            }
        }
    }
}