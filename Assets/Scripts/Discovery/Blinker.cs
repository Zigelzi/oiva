using System.Collections;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] Renderer _indicator;
        [SerializeField] float _blinkDuration = 1f;
        [SerializeField] bool _isBlinking = true;
        [SerializeField] float _blinkRate = 1.03f;

        Coroutine _blinking;
        Color _startColor;

        private void Awake()
        {
            if (_indicator == null) return;

            _startColor = _indicator.material.GetColor("_EmissionColor");
        }

        private void Start()
        {
            _blinking = StartCoroutine(Blink());
        }

        public void StopBlinking()
        {
            StopCoroutine(_blinking);
            _indicator.material.SetColor("_EmissionColor", _startColor);
        }
        private IEnumerator Blink()
        {
            Color currentColor = _indicator.material.GetColor("_EmissionColor");

            while (_isBlinking)
            {
                while (currentColor.a <= 19)
                {
                    _indicator.material.SetColor("_EmissionColor", currentColor * _blinkRate);
                    currentColor = _indicator.material.GetColor("_EmissionColor");
                    yield return null;
                }

                while (currentColor.a >= 0.1)
                {
                    _indicator.material.SetColor("_EmissionColor", currentColor / _blinkRate);
                    currentColor = _indicator.material.GetColor("_EmissionColor");
                    yield return null;
                }
            }
        }
    }
}