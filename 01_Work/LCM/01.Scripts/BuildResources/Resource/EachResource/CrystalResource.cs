using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource.EachResource
{
    public class CrystalResource : Resource
    {
        [SerializeField] private GameObject crystalVisual;

        protected override void Awake()
        {
            base.Awake();
            Instantiate(crystalVisual, transform.position + new Vector3(0, 0.26f, 0), Quaternion.identity, transform);
        }
    }
}