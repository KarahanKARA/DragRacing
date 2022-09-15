using System.Collections.Generic;
using Camera;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraController cameraController;
    [SerializeField] private List<GameObject> carPrefabs;
    
    
    private int _selectedGearType;
    public int SelectedGearType
    {
        get => _selectedGearType;
        set
        {
            if (value < 0 || value>1)
            {
                _selectedGearType = 0;
            }
            else
            {
                _selectedGearType = value;
            }
        }
    }

    private int _selectedCarIndex;
    public int SelectedCarIndex
    {
        get => _selectedCarIndex;
        set
        {
            if (value < 0 || value>1)
            {
                _selectedCarIndex = 0;
            }
            else
            {
                _selectedCarIndex = value;
            }
        }
    }

    private void Awake()
    {
        instance = this;
        var tempDataTransferScript = GameObject.FindGameObjectWithTag("DataTransfer").GetComponent<DataTransfer>();
        SelectedCarIndex = tempDataTransferScript.carType;
        SelectedGearType = tempDataTransferScript.gearType;
        
        var createdCarPrefab = Instantiate(carPrefabs[SelectedCarIndex], new Vector3(0,24,-173), Quaternion.identity);
        cameraController.PlayerTransform = createdCarPrefab.transform;
    }
}
