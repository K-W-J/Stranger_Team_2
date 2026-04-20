using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource.EachResource
{
    public class RockResource : Resource
    {
        [SerializeField] private List<GameObject> rockVisual;

        protected override void Awake()
        {
            base.Awake();
            int randIdx = Random.Range(0, rockVisual.Count);
            Instantiate(rockVisual[randIdx], transform.position + new Vector3(0, 0.27f, 0), Quaternion.identity,
                transform);
        }
    }
}