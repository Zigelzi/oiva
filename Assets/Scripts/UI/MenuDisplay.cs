using Oiva.Discovery;
using UnityEngine;

namespace Oiva.UI
{
    public class MenuDisplay : MonoBehaviour
    {
        CanvasGroup _canvasGroup;
        ParkingSpot _parkingSpot;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _parkingSpot = GameObject.FindGameObjectWithTag("Parking_Spot").GetComponent<ParkingSpot>();
            ToggleMenu(false);
        }

        private void OnEnable()
        {
            _parkingSpot.onAllScootersParked.AddListener(DisplayWinMenu);
        }


        private void OnDisable()
        {
            _parkingSpot.onAllScootersParked.RemoveListener(DisplayWinMenu);
        }

        private void DisplayWinMenu()
        {
            ToggleMenu(true);
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
    }
}
