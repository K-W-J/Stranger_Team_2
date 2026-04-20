using _01_Work.HS.Building;
using _01_Work.HS.Core.GameManagement.States;
using UnityEngine;

public class ResourceStorage : BuildObject
{
    public override void Build()
    {
        base.Build();
        ResourceManager.Instance.AddMaxCount(ResourceType.WOOD, 100);
        ResourceManager.Instance.AddMaxCount(ResourceType.STONE, 100);
    }
}
