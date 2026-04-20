using System;
using TMPro;
using UnityEngine;

public class ResourceCount : MonoBehaviour
{
    [SerializeField] private TMP_Text treeText;
    [SerializeField] private TMP_Text stoneText;
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text goldText;

    private ResourceManager _resource;

    private void Awake()
    {
        ResourceManager.Instance.OnValueChange += ChangeValue;
    }

    private void Start()
    {
        _resource = ResourceManager.Instance;
        ChangeValue();
    }

    private void ChangeValue()
    {
        string treeT = $"{_resource.GetCurResource(ResourceType.WOOD)} / {_resource.GetCurMaxResource(ResourceType.WOOD)}";
        string stoneT = $"{_resource.GetCurResource(ResourceType.STONE)} / {_resource.GetCurMaxResource(ResourceType.STONE)}";
        string crystalT = $"{_resource.GetCurResource(ResourceType.CRYSTAL)} / {_resource.GetCurMaxResource(ResourceType.CRYSTAL)}";
        string foodT = $"{_resource.GetCurResource(ResourceType.FOOD)} / {_resource.GetCurMaxResource(ResourceType.FOOD)}";
        string goldT = $"{_resource.GetCurResource(ResourceType.GOLD)}";

        treeText.text = treeT;
        stoneText.text = stoneT;
        crystalText.text = crystalT;
        foodText.text = foodT;
        goldText.text = goldT;
    }
}
