using _01_Work.HS.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ResourceType
{
    WOOD,
    STONE,
    CRYSTAL,
    FOOD,
    GOLD
}

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public Action OnValueChange;

    private Dictionary<ResourceType, int> _resourceDictionary = new Dictionary<ResourceType, int>();
    private Dictionary<ResourceType, int> _resourceMaxDictionary = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _resourceDictionary.Add(type, 0);
        }

        foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
        {
            _resourceMaxDictionary.Add(type, 100);
        }
        _resourceMaxDictionary[ResourceType.GOLD] = 100000000;
        _resourceMaxDictionary[ResourceType.FOOD] = 50;
        
        _resourceDictionary[ResourceType.GOLD] = 100;
        _resourceDictionary[ResourceType.FOOD] = 10;
        _resourceDictionary[ResourceType.WOOD] = 10;
        _resourceDictionary[ResourceType.STONE] = 10;
        _resourceDictionary[ResourceType.CRYSTAL] = 10;
    }

    private void Start()
    {
        OnValueChange.Invoke();
    }

    public bool CheckCanUse(int stoneCnt, int treeCnt, int crystalCnt, int foodCnt)
    {
        return _resourceDictionary[ResourceType.STONE] >= stoneCnt &&
               _resourceDictionary[ResourceType.WOOD] >= treeCnt &&
               _resourceDictionary[ResourceType.CRYSTAL] >= crystalCnt &&
               _resourceDictionary[ResourceType.FOOD] >= foodCnt;

    }

    public bool CheckCanGoldUse(int count) => _resourceDictionary[ResourceType.GOLD] >= count;

    public int GetCurResource(ResourceType type) => _resourceDictionary[type];

    public int GetCurMaxResource(ResourceType type) => _resourceMaxDictionary[type];

    public void AddResorce(ResourceType type, int count)
    {
        if(!_resourceDictionary.ContainsKey(type)) return;
            
        _resourceDictionary[type] += count;
        if (_resourceDictionary[type] > _resourceMaxDictionary[type])
            _resourceDictionary[type] = _resourceMaxDictionary[type];
        OnValueChange?.Invoke();
    }

    public void UseResource(ResourceType type, int count)
    {
        if (count > _resourceDictionary[type]) return;

        _resourceDictionary[type] -= count;
        OnValueChange?.Invoke();
    }

    public void AddMaxCount(ResourceType type, int count)
    {
        _resourceMaxDictionary[type] += count;
        OnValueChange?.Invoke();
    }

}
