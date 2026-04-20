using _01_Work.HS.Building;
using _01_Work.HS.Building.BuildingSO;
using _01_Work.HS.Core;
using _01_Work.HS.Core.GameManagement;
using _01_Work.HS.Core.Map;
using _01_Work.KHJ.CombatUnit;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum InfoType
{
    Unit,
    Building,
    Castle
}

public class InfoWindow : MonoSingleton<InfoWindow>
{
    [Header("InfoPanels")]
    [SerializeField] private GameObject unitInfo;
    [SerializeField] private GameObject castleInfo;
    [SerializeField] private GameObject buildingInfo;

    [SerializeField] private LayerMask whatIsGround;

    public Action OnClickedUpgrade;

    private Dictionary<InfoType, GameObject> _infoPanels;
    private BuildObject _curBuildObj;

    private InputSO _input;
    private bool _clickRequested;

    private int _castleLevel = 0;
    private int _castleHealth;

    private void Awake()
    {
        _infoPanels = new Dictionary<InfoType, GameObject>
        {
            { InfoType.Unit, unitInfo },
            { InfoType.Castle, castleInfo },
            { InfoType.Building, buildingInfo }
        };

        _input = GameManager.Instance.InputSO;

    }

    private void Start()
    {
        GameManager.Instance.OnChangeSelectObj += GetSelectObject;
        _input.OnClickEvent += OnClickRequested;
        GameManager.Instance.OnLevelUpEvent.AddListener(GetCastleLevel);
        GameManager.Instance.OnChangeHealthEvent.AddListener(GetCastleHealth);
        CloseAllPanel();
    }

    
    private void OnDisable()
    {
        _input.OnClickEvent -= OnClickRequested;
    }

    private void Update()
    {
        if (_clickRequested)
        {
            _clickRequested = false;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            HandleCheckGround();
        }
    }

    public void OnClickRequested()
    {
        _clickRequested = true;
    }

    private void HandleCheckGround()
    {
        RaycastHit hitInfo = _input.GetHitInfo(whatIsGround);

        if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Ground hitGround))
        {
            CloseAllPanel();
        }
    }

    private void GetCastleLevel(int value)
    {
        _castleLevel = value;
        GetSelectObject(_curBuildObj);
    }

    private void GetCastleHealth(int value)
    {
        _castleHealth = value;
        GetSelectObject(_curBuildObj);
    }

    public void GetSelectObject(SelectObject selectObj)
    {
        Debug.Log(selectObj);
        Debug.Log(_curBuildObj);
        if (selectObj is BuildObject buildObject)
        {
            if(buildObject.BuildingDataSO.buildingType == BuildingType.Castle)
                _curBuildObj = selectObj as BuildObject;
            else 
                _curBuildObj = null;
            
            CloseAllPanel();

            if (buildObject.BuildingDataSO.buildingType == BuildingType.Castle)
            {
                Transform info = _infoPanels[InfoType.Castle].transform;

                CastleDataSO data = _curBuildObj.BuildingDataSO as CastleDataSO;

                _infoPanels[InfoType.Castle].SetActive(true);
                info.Find("InfoWindowBg/NamePanel/Text_Name").GetComponent<TMP_Text>().text = buildObject.BuildingDataSO.buildingName;
                info.Find("InfoWindowBg/Slider").GetComponent<Slider>().SetValueWithoutNotify(((float)_castleHealth / data.health));
                info.Find("InfoWindowBg/Slider/Text_Hp").GetComponent<TMP_Text>().text = $"{_castleHealth} / {data.health}";
                info.Find("InfoWindowBg/Text_detail").GetComponent<TMP_Text>().text = buildObject.BuildingDataSO.description;
                info.Find("InfoWindowBg/Text_GetMoney").GetComponent<TMP_Text>().text =
                    $"버는 돈\n{data.salaryByLevel[_castleLevel]}";

                info.Find("InfoWindowBg/Text_Upgrade").GetComponent<TMP_Text>().text =
                    "업그레이드 재료\n" +
                    $"\n골드: {data.upgradeDataList[_castleLevel].gold}\n" +
                    $"나무: {data.upgradeDataList[_castleLevel].wood}\n" +
                    $"돌: {data.upgradeDataList[_castleLevel].stone}\n" +
                    $"크리스탈: {data.upgradeDataList[_castleLevel].crystal}\n" +
                    $"인구 수 : {data.upgradeDataList[_castleLevel].person}";
            }
            else
            {
                Transform info = _infoPanels[InfoType.Building].transform;
                _infoPanels[InfoType.Building].SetActive(true);
                info.Find("InfoWindowBg/NamePanel/Text_Name").GetComponent<TMP_Text>().text = buildObject.BuildingDataSO.buildingName;
                info.Find("InfoWindowBg/TextParent/Text_detail").GetComponent<TMP_Text>().text = buildObject.BuildingDataSO.description;
            }
        }
        else if (selectObj is CombatUnit combatUnit)
        {
            Transform info = _infoPanels[InfoType.Unit].transform;
            CloseAllPanel();
            unitInfo.SetActive(true);
            info.Find("InfoWindowBg/NamePanel/Text_Name").GetComponent<TMP_Text>().text = combatUnit.data.Name;
            info.Find("InfoWindowBg/Slider").GetComponent<Slider>().value = combatUnit._hp / combatUnit.data.MaxHp;
            info.Find("InfoWindowBg/Slider/Text_Hp").GetComponent<TMP_Text>().text = $"{combatUnit._hp} / {combatUnit.data.MaxHp}";
            info.Find("InfoWindowBg/Text_detail").GetComponent<TMP_Text>().text = combatUnit.data.ToolTip;
        }
    }
    
    public void CloseAllPanel()
    {
        foreach (var panel in _infoPanels.Values)
        {
            panel.SetActive(false);
        }
    }
}
