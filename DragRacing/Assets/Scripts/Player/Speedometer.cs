using TMPro;
using UnityEngine;

namespace Player
{
    public class Speedometer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedText;
        [SerializeField] private RectTransform arrowPivotTransform;
        private CarController _carController;
        private void Start()
        {
            _carController = GameManager.instance.playerCar.GetComponent<CarController>();
        }
        private void Update()
        {
            var currentSpeed = _carController.GetCurrentSpeed();
            arrowPivotTransform.rotation = Quaternion.Euler(180f, 0f,(90*currentSpeed)/130f);
            speedText.text = currentSpeed.ToString("0")+"\nKM/H";
        }
    }
}
