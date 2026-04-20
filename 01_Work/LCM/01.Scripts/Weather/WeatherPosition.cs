using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.Weather
{
    public class WeatherPosition : MonoBehaviour
    {
        public readonly Dictionary<string, ParticleSystem> ParticleDictionary = new Dictionary<string, ParticleSystem>();
        private Transform _parentRotation;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                ParticleDictionary.Add(transform.GetChild(i).name, 
                    transform.GetChild(i).GetComponent<ParticleSystem>());
            }
            _parentRotation = transform.parent.GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            Vector3 parentEuler = _parentRotation.rotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(-parentEuler.x, 0, 0);
        }
    }
}
