using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oiva.Status
{
    public class DestroyFXAfterEffect : MonoBehaviour
    {
        List<ParticleSystem> childParticles = new List<ParticleSystem>();
        List<ParticleSystem> aliveParticles = new List<ParticleSystem>();

        private void Awake()
        {
            GetChildParticles();
        }

        public void DestroyEffect(float waitTime)
        {
            StartCoroutine(DestroyFinishedFXWithChildren(waitTime));
        }

        public void DestroyEffectAfterDelay(float delay)
        {
            StartCoroutine(DestoryFinishedFX(delay));
        }


        private void GetChildParticles()
        {
            ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particle in particles)
            {
                childParticles.Add(particle);
                aliveParticles.Add(particle);
            }
        }

        private IEnumerator DestroyFinishedFXWithChildren(float waitTime)
        {
            while (aliveParticles.Count > 0)
            {
                foreach (ParticleSystem vfx in childParticles)
                {
                    DisableFinishedParticle(vfx);
                }
                yield return new WaitForSeconds(waitTime);
            }
            Destroy(gameObject);
        }

        private IEnumerator DestoryFinishedFX(float delay)
        {
            yield return new WaitForSeconds(delay);
            while (aliveParticles.Count > 0)
            {
                foreach (ParticleSystem vfx in childParticles)
                {
                    DisableFinishedParticle(vfx);
                }
                yield return null;
            }
            Destroy(gameObject);
        }

        private void DisableFinishedParticle(ParticleSystem vfx)
        {
            vfx.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            if (!vfx.IsAlive())
            {
                vfx.gameObject.SetActive(false);
                aliveParticles.Remove(vfx);
            }
        }
    }
}
