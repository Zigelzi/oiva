using Oiva.Control;
using Oiva.Discovery;
using TMPro;
using UnityEngine;


namespace Oiva.UI
{
    public class TotalEnergyCounter : MonoBehaviour
    {
        [SerializeField] TMP_Text _totalEnergyConsumedValue;

        Energy _energy;
        ParkingSpot _parkingSpot;


        private void Awake()
        {
            _energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
            _parkingSpot = GameObject.FindGameObjectWithTag("Parking_Spot").GetComponent<ParkingSpot>();
            _totalEnergyConsumedValue.text = "0";
        }

        private void OnEnable()
        {
            _energy.onEnergyExhausted.AddListener(UpdateTotalEnergyConsumed);
            _parkingSpot.onAllScootersParked.AddListener(UpdateTotalEnergyConsumed);
        }

        private void OnDisable()
        {
            _energy.onEnergyExhausted.RemoveListener(UpdateTotalEnergyConsumed);
            _parkingSpot.onAllScootersParked.RemoveListener(UpdateTotalEnergyConsumed);
        }

        void UpdateTotalEnergyConsumed()
        {
            if (_totalEnergyConsumedValue == null) return;

            _totalEnergyConsumedValue.text = _energy.TotalEnergyConsumed.ToString("0.0");
        }

        void UpdateTotalEnergyConsumed(float totalEnergyConsumed)
        {
            if (_totalEnergyConsumedValue == null) return;

            _totalEnergyConsumedValue.text = totalEnergyConsumed.ToString("0.0");
        }


    }
}
