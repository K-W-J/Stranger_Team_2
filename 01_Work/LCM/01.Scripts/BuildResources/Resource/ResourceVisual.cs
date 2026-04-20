using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource
{
    public class ResourceVisual : MonoBehaviour
    {
        [Header("GrowSetting")] 
        [SerializeField] protected float minGrowMultiplier;
        [SerializeField] protected float maxGrowMultiplier;

        [SerializeField] private float growDuration;

        private IEnumerator Start()
        {
            float rand = Random.Range(0f, 180f);
            transform.localRotation = Quaternion.Euler(0f, rand, 0f);
            
            float randGrowMultiplier = Random.Range(minGrowMultiplier, maxGrowMultiplier);
            
            yield return new DOTweenCYInstruction.WaitForCompletion(
                transform.DOScale(transform.localScale * randGrowMultiplier, growDuration));
            
            GetComponentInParent<Resource>().IsGrowEnd = true;
        }
    }
}