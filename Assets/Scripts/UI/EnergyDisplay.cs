using Oiva.Control;
using TMPro;
using UnityEngine;


namespace Oiva.UI
{
    public class EnergyDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text _energyValue;
        Energy _energy;

        private void Awake()
        {
            _energy = GameObject.FindGameObjectWithTag("Player").GetComponent<Energy>();
        }

        private void Start()
        {
            _energyValue.text = _energy.CurrentEnergy.ToString("0.0");
        }

        private void OnEnable()
        {
            _energy.onEnergyChange.AddListener(UpdateEnergyValue);
        }

        private void OnDisable()
        {
            _energy.onEnergyChange.RemoveListener(UpdateEnergyValue);
        }

        void UpdateEnergyValue(float newValue)
        {
            if (_energyValue == null) return;

            _energyValue.text = newValue.ToString("0.0");
        }


    }
}
