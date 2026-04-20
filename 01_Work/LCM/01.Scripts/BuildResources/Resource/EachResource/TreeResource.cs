using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource.EachResource
{
    public class TreeResource : Resource
    {
        [SerializeField] private int resourceVisualCount;
        [SerializeField] private List<GameObject> treeVisual;
        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < resourceVisualCount; i++)
            {
                float rand = Random.Range(-0.15f, 0.15f);
                int randIdx = Random.Range(0, treeVisual.Count);
                Instantiate(treeVisual[randIdx], transform.position + new Vector3(rand, 0.26f, rand), Quaternion.identity,transform);
            }
        }
    }
}
