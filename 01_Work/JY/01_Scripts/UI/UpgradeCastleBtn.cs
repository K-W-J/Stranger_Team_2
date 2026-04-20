using _01_Work.HS.Building.BuildingSO;
using _01_Work.HS.BuildingSystem.Building;
using _01_Work.HS.Core.GameManagement;
using UnityEngine;

public class UpgradeCastleBtn : MonoBehaviour
{
    public BuildingType _buildType { get; set; }
    private CastleDataSO _data;
    private Castle _castle;

    private void Start()
    {
        _data = ShopManager.Instance.GetBuildingData(BuildingType.Castle) as CastleDataSO;
    }

    public void UpgradeCastle()
    {
        _castle = GameManager.Instance.Castle;
        _castle.Upgrade();
    }
}
