using System.Collections;
using UnityEngine;

namespace Oiva.Discovery
{
    public class Blinker : MonoBehaviour
    {
        [SerializeField] Renderer _indicator;
        [SerializeField] float _blinkDuration = 1f;
        [SerializeField] bool _isBlinking = true;

        private void Start()
        {
            StartCoroutine(Blink());
        }

        public void StopBlinking()
        {
            StopCoroutine(Blink());
        }
        private IEnumerator Blink()
        {
            Color currentColor = _indicator.material.GetColor("_EmissionColor");

            while (_isBlinking)
            {
                while (currentColor.a <= 19)
                {
                    _indicator.material.SetColor("_EmissionColor", currentColor * 1.02f);
                    currentColor = _indicator.material.GetColor("_EmissionColor");
                    yield return null;
                }

                while (currentColor.a >= 0.1)
                {
                    _indicator.material.SetColor("_EmissionColor", currentColor / 1.02f);
                    currentColor = _indicator.material.GetColor("_EmissionColor");
                    yield return null;
                }
            }
        }
    }
}