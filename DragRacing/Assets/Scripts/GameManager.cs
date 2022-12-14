using System.Collections;
using System.Collections.Generic;
using Camera;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerCar;
    public CameraController cameraController;
    [SerializeField] private List<GameObject> carPrefabs;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI raceTimeText;
    [SerializeField] private GameObject endgamePanel;

    private GameObject _endPointObj;
    private float _raceTime = 0;


    private bool _isPlayerFinished;

    public bool IsPlayerFinished
    {
        get => _isPlayerFinished;
        set => _isPlayerFinished = value;
    }

    private bool _canCalculateRaceTime;

    public bool CanCalculateRaceTime
    {
        get => _canCalculateRaceTime;
        set => _canCalculateRaceTime = value;
    }

    private bool _canPlayerRace;

    public bool CanPlayerRace
    {
        get => _canPlayerRace;
        set => _canPlayerRace = value;
    }

    private int _selectedMapType;
    public int SelectedMapType
    {
        get => _selectedMapType;
        set
        {
            if (value < 0 || value > 1)
            {
                _selectedMapType = 0;
            }
            else
            {
                _selectedMapType = value;
            }
        }
    }
    
    private int _selectedGearType;

    public int SelectedGearType
    {
        get => _selectedGearType;
        set
        {
            if (value < 0 || value > 1)
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
        set => _selectedCarIndex = value;
    }


    private void Awake()
    {
        instance = this;
        CanPlayerRace = false;
        CanCalculateRaceTime = false;
        var tempDataTransferScript = GameObject.FindGameObjectWithTag("DataTransfer").GetComponent<DataTransfer>();
        SelectedCarIndex = tempDataTransferScript.carType;
        SelectedGearType = tempDataTransferScript.gearType;
        SelectedMapType = tempDataTransferScript.mapType;

        var createdCarPrefab = Instantiate(carPrefabs[SelectedCarIndex], new Vector3(0, 24, -174), Quaternion.identity);
        cameraController.PlayerTransform = createdCarPrefab.transform;
        playerCar = createdCarPrefab;
        StartCoroutine(CountDown());
        _endPointObj = GameObject.FindGameObjectWithTag("EndPoint");
        playerCar = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (CanCalculateRaceTime)
        {
            _raceTime += Time.deltaTime;
            raceTimeText.text = "TIME: " + _raceTime.ToString("0.00");
            if (playerCar.transform.position.z >= _endPointObj.transform.position.z)
            {
                RaceCompleted();
            }
        }
    }

    private IEnumerator CountDown()
    {
        int count = 5;
        while (count > 0)
        {
            countDownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countDownText.text = "GO!";
        CanCalculateRaceTime = true;
        CanPlayerRace = true;
        raceTimeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        countDownText.gameObject.SetActive(false);
    }

    private void RaceCompleted()
    {
        IsPlayerFinished = true;
        CanPlayerRace = false;
        CanCalculateRaceTime = false;
        raceTimeText.GetComponent<HeartBeatEffectUI>().enabled = true;
        StartCoroutine(ShowEndgamePanel());
    }

    private IEnumerator ShowEndgamePanel()
    {
        yield return new WaitForSeconds(3f);
        endgamePanel.SetActive(true);
    }

    public void EndgameButtonsClicked(int i)
    {
        if (i == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (i == 1)
        {
            var dataTransferObj = GameObject.FindGameObjectWithTag("DataTransfer");
            if (dataTransferObj!=null)
            {
                Destroy(dataTransferObj);
            }
            SceneManager.LoadScene(0);
        }
    }
}