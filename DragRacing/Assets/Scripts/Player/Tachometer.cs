using TMPro;
using UnityEngine;

namespace Player
{
    public class Tachometer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rpmText;
        [SerializeField] private TextMeshProUGUI gearText;
        [SerializeField] private RectTransform arrowPivotTransform;

        private CarController _carController;

        private void Start()
        {
            _carController = GameManager.instance.playerCar.GetComponent<CarController>();
        }

        private void Update()
        {
            rpmText.text = _carController.CurrentRpm.ToString("0") + "\nRPM";
            gearText.text = _carController.CurrentGearTier + "\nGEAR";
            var scale = _carController.CurrentRpm % 1000;
            var tempRadius = ((scale * 245) / 1000) - 35;
            arrowPivotTransform.rotation = Quaternion.Euler(180f, 0f, tempRadius);
        }
    }
}