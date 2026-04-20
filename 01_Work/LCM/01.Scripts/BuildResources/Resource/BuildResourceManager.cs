using System.Collections.Generic;
using _01_Work.HS.Core;
using _01_Work.HS.Core.GameManagement;
using _01_Work.HS.Core.Map;
using _01_Work.LCM._01.Scripts.Day;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _01_Work.LCM._01.Scripts.BuildResources.Resource
{
    public class BuildResourceManager : MonoSingleton<BuildResourceManager>
    {
        [Header("Resource")] public List<Resource> resources;

        [SerializeField] private int waitingCreateTime;

        private Vector3Int _randomKey;
        private Dictionary<Vector3Int, Ground> _grounds;
        private List<Vector3Int> _keys;

        public bool IsCanSetResource { get; set; } = true;
        
        public Queue<GameObject> takeResources = new Queue<GameObject>();

        private void Awake()
        {
            DayManager.Instance.OnChangeMorning += HandleCreateResources;
        }
        
        private async void Start()
        {
            _grounds = GameManager.Instance.GroundList;
            await Awaitable.WaitForSecondsAsync(0.2f);
            CreateResources(80);
        }

        private void HandleCreateResources()
        {
            CreateResources(20 + (DayManager.Instance.CurrentDay / 3));
        }

        public void CreateResources(int count = 10)
        {
            _keys = new List<Vector3Int>(_grounds.Keys);

            for (int i = 0; i < count; i++)
            {
                _randomKey = _keys[Random.Range(0, _keys.Count)];

                if (_grounds.Count > 0 && _grounds[_randomKey].IsCanPlace)
                {
                    int randIndex = Random.Range(0, resources.Count);
                    
                    Resource resource = Instantiate(resources[randIndex], _grounds[_randomKey].transform.position, Quaternion.identity,
                        transform);
                    _grounds[_randomKey].SetPlaceObject(resource);
                    resource.Setup(_grounds[_randomKey]);
                }
            }
        }
    }
}