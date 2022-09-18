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
        private const float RotationSpeed = 0.01f;
        private int _numberOfGears;
        private float _timeCount = 0.0f;

        private void Start()
        {
            _carController = GameManager.instance.playerCar.GetComponent<CarController>();
            _numberOfGears = _carController.GetNumberOfGears();
        }

        private void Update()
        {
            _timeCount += Time.deltaTime;
            rpmText.text = _carController.CurrentRpm.ToString("0") + "\nRPM";
            gearText.text = _carController.CurrentGearTier.ToString();
            var scale = _carController.CurrentRpm % 1000;
            var perGearRadius = 240f / (_numberOfGears);
            var degreeDifference = (perGearRadius * (_carController.CurrentGearTier - 1));
            var tempRadius = (scale * (240 - degreeDifference) / 1000) - 35 + degreeDifference;
            var currentRot = arrowPivotTransform.transform.rotation;
            var targetRot = Quaternion.Euler(180, 0, tempRadius);
            if (_carController.CurrentGearTier >= _carController.GetGearMustBe())
            {
                arrowPivotTransform.rotation = Quaternion.Lerp(currentRot, targetRot, _timeCount * RotationSpeed);
            }
        }
    }
}