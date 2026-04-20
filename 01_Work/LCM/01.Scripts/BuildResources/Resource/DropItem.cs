using UnityEngine;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource
{
    public class DropItem : MonoBehaviour
    {
        [field:SerializeField] public ResourceType DropItemType { get; private set; }
    }
}
