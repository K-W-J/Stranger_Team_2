using _01_Work.HS.Building;
using UnityEngine;

public class FoodStorage : BuildObject
{
    public override void Build()
    {
        base.Build();
        ResourceManager.Instance.AddMaxCount(ResourceType.FOOD, 100);
    }
}
