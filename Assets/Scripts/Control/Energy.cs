using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Oiva.Control
{
    public class Energy : MonoBehaviour
    {
        [SerializeField] float _maxEnergy = 100f;
        [SerializeField] float _currentEnergy = -1f;
        [SerializeField][Range(0, 20)] float _initialEnergyConsumption = 1f;
        [Tooltip("How much movement is required to consume energy")]
        [SerializeField] float _movementTolerance = .2f;

        float _currentEnergyConsumption = -1f;
        float _totalEnergyConsumed = 0f;
        float _storedEnergyChange = 0;
        bool _isEnergyReductionActive = false;
        Coroutine _currentEnergyReductionEffect;
        Rigidbody _rb;

        public EnergyChange onEnergyChange;
        public EnergyExhausted onEnergyExhausted;

        [Serializable]
        public class EnergyChange : UnityEvent<float> { }

        [Serializable]
        public class EnergyExhausted : UnityEvent<float> { }
        public float CurrentEnergy { get { return _currentEnergy; } }
        public float TotalEnergyConsumed { get { return _totalEnergyConsumed; } }

        private void Awake()
        {
            _currentEnergy = _maxEnergy;
            _currentEnergyConsumption = _initialEnergyConsumption;
            _totalEnergyConsumed = 0f;
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Consume();

            if (_currentEnergy <= 0)
            {
                _currentEnergy = 0;
                onEnergyChange?.Invoke(_currentEnergy);
                onEnergyExhausted?.Invoke(_totalEnergyConsumed);
            }
        }

        public void StopEnergyConsumptionTemporarily(float duration)
        {
            if (_currentEnergyReductionEffect != null)
            {
                StopCoroutine(_currentEnergyReductionEffect);
            }
            _currentEnergyReductionEffect = StartCoroutine(StopEnergyConsumption(duration));
        }

        public void ChangeEnergyConsumptionPermanently(float amount)
        {
            _storedEnergyChange = amount;
            _currentEnergyConsumption = AdjustEnergyConsumption();
        }

        public void RestoreDefaultEnergyConsumption()
        {
            _currentEnergyConsumption = _initialEnergyConsumption;
        }

        private IEnumerator StopEnergyConsumption(float duration)
        {
            float durationRemaining = duration;
            _isEnergyReductionActive = true;

            _currentEnergyConsumption = AdjustEnergyConsumption();
            while (durationRemaining >= 0)
            {
                durationRemaining -= Time.deltaTime;
                yield return null;
            }
            _isEnergyReductionActive = false;
            _currentEnergyConsumption = AdjustEnergyConsumption();
        }

        private float AdjustEnergyConsumption()
        {
            float energyChange = _storedEnergyChange;

            if (_isEnergyReductionActive)
            {
                return 0;
            }
            else if (energyChange > 0)
            {
                _storedEnergyChange = 0;
                return _initialEnergyConsumption + energyChange;
            }
            else
            {
                return _initialEnergyConsumption;
            }

        }

        private void Consume()
        {
            if (_rb.velocity.sqrMagnitude > _movementTolerance)
            {
                float consumedEnergy = _currentEnergyConsumption * Time.deltaTime;
                _currentEnergy -= consumedEnergy;
                _totalEnergyConsumed += consumedEnergy;
                onEnergyChange?.Invoke(_currentEnergy);
            }
        }
    }

}