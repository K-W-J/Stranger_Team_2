using _01_Work.HS.Core.GameManagement;
using UnityEngine;

public class ChangeGatheringBtn : MonoBehaviour
{
    public void ChangeGathering()
    {
        GameManager.Instance.IsGathering = !GameManager.Instance.IsGathering;
    }
}
