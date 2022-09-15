using UnityEngine;

namespace Player
{
    public class Speedometer : MonoBehaviour
    {
        private RectTransform _arrowPivot;
        private CarController _carController;
        private void Start()
        {
            _carController = GameManager.instance.playerCar.GetComponent<CarController>();
            _arrowPivot = GetComponent<RectTransform>();
        }
        private void Update()
        {
            var currentSpeed = _carController.GetCurrentSpeed();
            _arrowPivot.rotation = Quaternion.Euler(180f, 0f,(90*currentSpeed)/130f);
        }
    }
}
