using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Weather
{
    public class WeatherParticle : MonoBehaviour
    {
        [SerializeField] private WeatherSO weatherSO;
        private ParticleSystem[] _particleSystems;
        private void OnValidate()
        {
            gameObject.name = weatherSO.name;
        }

        private void Awake()
        {
            _particleSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem particle in _particleSystems)
            {
                var mainModule = particle.main;
                mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
                mainModule.playOnAwake = false;
                particle.Stop();
            }
        }
    }
}
