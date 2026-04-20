using _01_Work.HS.Building.BuildingSO;
using _01_Work.HS.Core;
using _01_Work.HS.Core.GameManagement;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopManager : MonoSingleton<ShopManager>
{
    [SerializeField] private BuildingDataSOList buildingDataSOList;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform content;

    private float _oldYPos;

    private Dictionary<BuildingType, BuildingDataSO> _buildingDataSODic = new Dictionary<BuildingType, BuildingDataSO>();

    private void Awake()
    {
        foreach (BuildingDataSO buildingData in buildingDataSOList.buildDataSOList)
        {
            _buildingDataSODic.Add(buildingData.buildingType, buildingData);
        }
        SpawnUnitItem();
    }

    private void Start()
    {
        _oldYPos = transform.position.y;
    }

    public void SpawnUnitItem()
    {
        ClearShopItems();
        foreach (BuildingDataSO buildingData in buildingDataSOList.buildDataSOList)
        {
            if (buildingData.buildingCategory == BuildingCategory.Unit)
            {
                GameObject item = Instantiate(shopItemPrefab, content);
                BuildItem buildItem = item.GetComponent<BuildItem>();
                buildItem._buildType = buildingData.buildingType;
                if (buildingData.buildingType == BuildingType.Castle)
                    Destroy(item);
            }
        }
    }

    public void SpawnBattleItem()
    {
        ClearShopItems();
        foreach (BuildingDataSO buildingData in buildingDataSOList.buildDataSOList)
        {
            if (buildingData.buildingCategory == BuildingCategory.Battle)
            {
                GameObject item = Instantiate(shopItemPrefab, content);
                BuildItem buildItem = item.GetComponent<BuildItem>();
                buildItem._buildType = buildingData.buildingType;
                if (buildingData.buildingType == BuildingType.Castle)
                    Destroy(item);
            }
        }
    }

    public void SpawnResourceItem()
    {
        ClearShopItems();
        foreach (BuildingDataSO buildingData in buildingDataSOList.buildDataSOList)
        {
            if (buildingData.buildingCategory == BuildingCategory.Resource)
            {
                GameObject item = Instantiate(shopItemPrefab, content);
                BuildItem buildItem = item.GetComponent<BuildItem>();
                buildItem._buildType = buildingData.buildingType;
                if (buildingData.buildingType == BuildingType.Castle)
                    Destroy(item);
            }
        }
    }

    public void SpawnEconomyItem()
    {
        ClearShopItems();
        foreach (BuildingDataSO buildingData in buildingDataSOList.buildDataSOList)
        {
            if (buildingData.buildingCategory == BuildingCategory.Economy)
            {
                GameObject item = Instantiate(shopItemPrefab, content);
                BuildItem buildItem = item.GetComponent<BuildItem>();
                buildItem._buildType = buildingData.buildingType;
                if (buildingData.buildingType == BuildingType.Castle)
                    Destroy(item);
            }
        }
    }

    public void SpawnBuilding(BuildingType type)
    {
        GameManager.Instance.SetBuilding(type);
    }

    public BuildingDataSO GetBuildingData(BuildingType type)
    {
        return _buildingDataSODic[type];
    }

    public void SetMoving(bool value)
    {
        if(!value)
            transform.DOMoveY(-100, 0.5f);
        else if (value)
            transform.DOMoveY(_oldYPos, 0.5f);
    }
    public void ClearShopItems()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
}
