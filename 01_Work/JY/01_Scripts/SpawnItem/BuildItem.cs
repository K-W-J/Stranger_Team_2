using _01_Work.HS.Core.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using _01_Work.HS.Building.BuildingSO;
using UnityEngine.UI;
using System;

public class BuildItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage; 
    public BuildingType _buildType { get; set; }
    private BuildingDataSO _data;
    private bool _isPointerOver = false;

    private void Start()
    {
        _data = ShopManager.Instance.GetBuildingData(_buildType);
        itemImage.sprite = _data.buildingIcon;
        CheckResource();
        ResourceManager.Instance.OnValueChange += CheckResource;
    }

    private void CheckResource()
    {
        bool canBuy = CheckCanBuy();
        bool canBuild = GameManager.Instance.CheckCanBuild(_buildType);

        if (canBuy && canBuild && itemImage != null)
        {
            itemImage.color = Color.white;
            if (_isPointerOver)
                BuildingPriceInfoText.Instance.canText.color = new Color32(0, 200, 45, 255);
        }
        else if (itemImage != null)
        {
            itemImage.color = Color.black;
            if (_isPointerOver)
                BuildingPriceInfoText.Instance.canText.color = Color.red;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CheckCanBuy() && GameManager.Instance.CheckCanBuild(_buildType))
        {
            buyItem();
            ShopManager.Instance.SpawnBuilding(_buildType);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true;
        transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f);
        BuildingPriceInfoText.Instance.ShowPriceText(gameObject, _data.buildingName, _data.needWood, _data.needStone, _data.needCrystal, _data.needGold, _data.needFood, _data.description);
        CheckResource();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false;
        BuildingPriceInfoText.Instance.ClosePanels();
        transform.DOScale(Vector3.one, 0.1f);
    }

    private bool CheckCanBuy()
    {
        if (ResourceManager.Instance.GetCurResource(ResourceType.WOOD) < _data.needWood) return false;
        if (ResourceManager.Instance.GetCurResource(ResourceType.STONE) < _data.needStone) return false;
        if (ResourceManager.Instance.GetCurResource(ResourceType.CRYSTAL) < _data.needCrystal) return false;
        if (ResourceManager.Instance.GetCurResource(ResourceType.FOOD) < _data.needFood) return false;
        if (ResourceManager.Instance.GetCurResource(ResourceType.GOLD) < _data.needGold) return false;

        return true;
    }

    private void buyItem()
    {
        ResourceManager.Instance.UseResource(ResourceType.WOOD, _data.needWood);
        ResourceManager.Instance.UseResource(ResourceType.STONE, _data.needStone);
        ResourceManager.Instance.UseResource(ResourceType.CRYSTAL, _data.needCrystal);
        ResourceManager.Instance.UseResource(ResourceType.FOOD, _data.needFood);
        ResourceManager.Instance.UseResource(ResourceType.GOLD, _data.needGold);
    }
}
