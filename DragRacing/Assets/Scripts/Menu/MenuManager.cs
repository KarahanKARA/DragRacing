using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private DataTransfer dataTransfer;
        [SerializeField] private List<GameObject> cars;
        [SerializeField] private List<GameObject> mapsCards;
        [SerializeField] private List<GameObject> gearCards;
        [SerializeField] private GameObject carSelectionScreen;
        [SerializeField] private GameObject mapSelectionScreen;
        [SerializeField] private GameObject gearSelectionScreen;
        private int _currentActiveCarIndex;
        private int _currentSelectedMapIndex = -1;
        private int _currentSelectedGearIndex = -1;


        private void Awake()
        {
            cars.ForEach((x) => _currentActiveCarIndex = x.activeSelf ? cars.IndexOf(x) : _currentActiveCarIndex);
        }
        public void ChangeCar(int direction)
        {
            cars[_currentActiveCarIndex].SetActive(false);
            cars[_currentActiveCarIndex].transform.rotation = Quaternion.identity;
            
            if (direction == 0) // left
            {
                _currentActiveCarIndex = _currentActiveCarIndex-1 < 0 ? cars.Count-1 : _currentActiveCarIndex-1;
            }
            else // right
            {
                _currentActiveCarIndex = _currentActiveCarIndex+1 > cars.Count-1 ? 0 : _currentActiveCarIndex+1;
            }
            cars[_currentActiveCarIndex].SetActive(true);
        }

        public void CarSelectionButtonPressed()
        {
            cars[_currentActiveCarIndex].GetComponent<RotateObject>().enabled = false;
            cars[_currentActiveCarIndex].transform.rotation = Quaternion.identity;
            var attributeList = GameObject.FindGameObjectsWithTag("Attribute").ToList();
            carSelectionScreen.SetActive(false);
            mapSelectionScreen.SetActive(true);
            attributeList.ForEach((x) => x.SetActive(false));
        }
        public void MapCardPressed(int clickedButtonIndex)
        {
            Color32 temp = default;
            foreach (var x in mapsCards)
            {
                temp = x.GetComponent<Image>().color;
                temp.a = 40;
                x.GetComponent<Image>().color = temp;
            }
            temp.a = 255;
            mapsCards[clickedButtonIndex].GetComponent<Image>().color = temp;
            _currentSelectedMapIndex = clickedButtonIndex;
        }
        public void MapSelectionButtonPressed()
        {
            if (_currentSelectedMapIndex != -1)
            {
                mapSelectionScreen.SetActive(false);
                gearSelectionScreen.SetActive(true);
            }
        }
        public void GearCardPressed(int clickedButtonIndex)
        {
            Color32 temp = default;
            foreach (var x in gearCards)
            {
                temp = x.GetComponent<Image>().color;
                temp.a = 40;
                x.GetComponent<Image>().color = temp;
            }
            temp.a = 255;
            gearCards[clickedButtonIndex].GetComponent<Image>().color = temp;
            _currentSelectedGearIndex = clickedButtonIndex;
        }
        public void GearSelectionPressed()
        {
            if (_currentSelectedGearIndex != -1)
            {
                dataTransfer.carType = _currentActiveCarIndex;
                dataTransfer.gearType = _currentSelectedGearIndex;
                DontDestroyOnLoad(dataTransfer.gameObject);
                SceneManager.LoadScene(_currentSelectedMapIndex+1);
                
            }
        }
    }
}