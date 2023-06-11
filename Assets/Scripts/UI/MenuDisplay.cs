using Oiva.Control;
using Oiva.Discovery;
using UnityEngine;

namespace Oiva.UI
{
    public class MenuDisplay : MonoBehaviour
    {
        [SerializeField] RectTransform _winSection;
        [SerializeField] RectTransform _defeatSection;
        CanvasGroup _canvasGroup;
        ParkingSpot _parkingSpot;
        Energy _energy;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _parkingSpot = GameObject.FindGameObjectWithTag("Parking_Spot").GetComponent<ParkingSpot>();
            _energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
            ToggleMenu(false);
            ToggleWinSection(false);
            ToggleDefeatSection(false);
        }

        private void OnEnable()
        {
            _parkingSpot.onAllScootersParked.AddListener(DisplayWinMenu);
            _energy.onEnergyExhausted.AddListener(DisplayDefeatMenu);
        }


        private void OnDisable()
        {
            _parkingSpot.onAllScootersParked.RemoveListener(DisplayWinMenu);
            _energy.onEnergyExhausted.RemoveListener(DisplayDefeatMenu);
        }

        private void DisplayWinMenu()
        {
            ToggleMenu(true);
            ToggleWinSection(true);
        }

        private void DisplayDefeatMenu()
        {
            ToggleMenu(true);
            ToggleDefeatSection(true);
        }



        private void ToggleMenu(bool isEnabled)
        {
            if (_canvasGroup == null) return;

            if (isEnabled)
            {
                _canvasGroup.alpha = 1;
            }
            else
            {
                _canvasGroup.alpha = 0;
            }

            _canvasGroup.interactable = isEnabled;
            _canvasGroup.blocksRaycasts = isEnabled;
        }

        private void ToggleWinSection(bool isEnabled)
        {
            if (_winSection == null) return;

            _winSection.gameObject.SetActive(isEnabled);
        }

        private void ToggleDefeatSection(bool isEnabled)
        {
            if (_winSection == null) return;

            _defeatSection.gameObject.SetActive(isEnabled);
        }
    }
}
