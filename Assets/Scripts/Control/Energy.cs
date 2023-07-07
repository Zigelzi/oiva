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
        Coroutine _currentEnergyEffect;
        Rigidbody _rb;

        public EnergyChange onEnergyChange;
        public UnityEvent onEnergyExhausted;

        [Serializable]
        public class EnergyChange : UnityEvent<float> { }
        public float CurrentEnergy { get { return _currentEnergy; } }

        private void Awake()
        {
            _currentEnergy = _maxEnergy;
            _currentEnergyConsumption = _initialEnergyConsumption;
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

        public void StopEnergyConsumptionTemporarily(float duration)
        {
            if (_currentEnergyEffect != null)
            {
                StopCoroutine(_currentEnergyEffect);
            }
            _currentEnergyEffect = StartCoroutine(StopEnergyConsumption(duration));
        }

        public void RestoreDefaultEnergyConsumption()
        {
            _currentEnergyConsumption = _initialEnergyConsumption;
        }

        private IEnumerator StopEnergyConsumption(float duration)
        {
            float durationRemaining = duration;

            _currentEnergyConsumption = 0;
            while (durationRemaining >= 0)
            {
                durationRemaining -= Time.deltaTime;
                yield return null;
            }
            _currentEnergyConsumption = _initialEnergyConsumption;
        }

        private void Consume()
        {
            if (_rb.velocity.sqrMagnitude > _movementTolerance)
            {
                _currentEnergy -= _currentEnergyConsumption * Time.deltaTime;
                onEnergyChange?.Invoke(_currentEnergy);
            }
        }
    }

}