using _01_Work.KWJ._01_Scripes.WorkingUnit;
using System;
using TMPro;
using UnityEngine;

public class UnitCount : MonoBehaviour
{
    [SerializeField] private TMP_Text unitText;
    [SerializeField] private TMP_Text restText;

    private WorkingUnitManager _unitManager;

    private void Awake()
    {
        WorkingUnitManager.Instance.OnMaxUnitChanged += ChangeUnitCount;
    }

    private void Start()
    {
        _unitManager = WorkingUnitManager.Instance;
    }

    private void ChangeUnitCount()
    {
        string unitT = $"{_unitManager.MaxWorkingUnitCount}";
        string restT = $"{_unitManager.UnemployedUnitList.Count}";

        unitText.text = unitT;
        restText.text = restT;
    }
}
