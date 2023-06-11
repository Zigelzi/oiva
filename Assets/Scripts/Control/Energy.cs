using System;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] float _maxEnergy = 100f;
        [SerializeField] float _currentEnergy = -1f;
        [SerializeField][Range(0, 20)] float _energyConsumption = 1f;
        [Tooltip("How much movement is required to consume energy")]
        [SerializeField] float _movementTolerance = .2f;

        Rigidbody _rb;

        public EnergyChange onEnergyChange;
        public UnityEvent onEnergyExhausted;

        [Serializable]
        public class EnergyChange : UnityEvent<float> { }
        public float CurrentEnergy { get { return _currentEnergy; } }

        private void Awake()
        {
            _currentEnergy = _maxEnergy;
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Consume();

            if (_currentEnergy <= 0)
            {
                _currentEnergy = 0;
                onEnergyChange?.Invoke(_currentEnergy);
                onEnergyExhausted?.Invoke();
            }
        }

        private void Consume()
        {
            if (_rb.velocity.sqrMagnitude > _movementTolerance)
            {
                _currentEnergy -= _energyConsumption * Time.deltaTime;
                onEnergyChange?.Invoke(_currentEnergy);
            }
        }
    }

}