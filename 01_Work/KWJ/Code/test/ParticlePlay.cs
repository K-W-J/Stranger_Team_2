using System;
using System.Collections;
using UnityEngine;

namespace _01_Work.KWJ._01_Scripes.test
{
    public class ParticlePlay : MonoBehaviour
    {
        [SerializeField] private ParticleSystem effect;

        private void Start()
        {
            effect.Clear();
            StartCoroutine(Delay());
        }
        
        IEnumerator Delay()
        {
            effect.Play();
            yield return new WaitForSeconds(1.5f);
            effect.Stop();
            Destroy(gameObject);
        }
    }
}