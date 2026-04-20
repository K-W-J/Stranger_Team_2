﻿using System;
using System.Collections;
using System.Linq;
using _01_Work.HS.Building;
using _01_Work.HS.Building.BuildingSO;
using _01_Work.LCM._01.Scripts.BuildResources.Resource;
using _01_Work.LCM._01.Scripts.Weather;
using KWJ.Unit;
using UnityEngine;
using Works.KWJ.WorkingUnit;
using Random = UnityEngine.Random;

namespace _01_Work.KWJ._01_Scripes.WorkingUnit
{
    public enum WorkType
    {
        None = -1,
        
        Porter,
        ResourceMiner,
        Archer,
        Farmer,
        Repairman,
        Tavernkeeper,
        Priest,
        Parkranger,
        Warrior,
        Bankteller,
        
        /*짐꾼,
        자원채굴꾼,
        궁수,
        농부,
        수리공,
        선술집 주인,
        사제,
        공원관리자,
        전사
        은행원*/
        
        Max
    }
    public class WorkingUnit : Units
    {
        public GameObject WorkTarget { get; set; }
        
        public GameObject ResoursTarget { get; set; }
        [field: SerializeField] public WorkType WorkType { get; set; } = WorkType.None; 
        
        [field: SerializeField] public GameObject Visual { get; set; }
        
        [SerializeField] private WorkingUnitDataSO workingUnitDataSo;
        
        [SerializeField] private new GameObject particleSystem;
        
        [SerializeField] private Transform catchPostiton;
        
        private bool _isDelay = true;
        private bool _isMoveDelay = true;
        public bool IsReset = true;
        
        private float _moveSpeed;
        private float _bounceAmplitud;
        private float _bounceFrequency;
        private float _workSpeed;
        private int _workPower;
        
        private int _stoneCollection = 8;
        private int _crystalCollection = 5;
        private int _woodCollection = 10;
        private int _foodCollection = 5;
        
        private Vector3 _initialLocalPosition;
        
        private float xRandomPos;
        private float yRandomPos; 

        protected override void Awake()
        {
            base.Awake();
            
            /*if(WeatherManager.Instance != null)
                WeatherManager.Instance.OnChangeWeather += ChangeWeater;*/
            
            SettingUnitData();
        }
        private void SettingUnitData()
        {
            _moveSpeed = workingUnitDataSo.MoveSpeed;
            _bounceAmplitud = workingUnitDataSo.BounceAmplitude;
            _bounceFrequency = workingUnitDataSo.BounceFrequency;
            
            _workSpeed = workingUnitDataSo.WorkSpeed;
            _workPower = workingUnitDataSo.WorkPower;
            
            _stoneCollection = workingUnitDataSo.StoneCollection;
            _crystalCollection = workingUnitDataSo.CrystalCollection;
            _woodCollection = workingUnitDataSo.WoodCollection;
            _foodCollection = workingUnitDataSo.FoodCollection;
        }
        
        protected override void Start()
        {
            base.Start();
            
            UnitMovment.MovementSetting(_moveSpeed);
            
            if (Visual != null)
                _initialLocalPosition = Visual.transform.localPosition;
        }

        protected override void Update()
        {
            base.Update();
            
            UnitMovement();
        }
        private void OnDisable()
        { 
            //if(WeatherManager.Instance != null)
                //WeatherManager.Instance.OnChangeWeather -= ChangeWeater;
        }
        
        private void UnitMovement()
        {
            if (!UnitMovment.IsMovementStop())
            {
                Visual.SetActive(true);
            }
            
            if(WorkType == WorkType.None || WorkType == WorkType.Farmer || WorkType == WorkType.Priest)
            {
                Vector3 castlePos = Vector3.zero;
                
                if(WorkType == WorkType.None)
                    castlePos = WorkingUnitManager.Instance.CastleBuilding.transform.position;
                else if(WorkType == WorkType.Farmer || WorkType == WorkType.Priest)
                    castlePos = WorkTarget.transform.position;
                
                if (UnitMovment.IsMovementStop() && _isMoveDelay)
                {
                    StartCoroutine(MoveDelay());
                }

                UnitMovment.Move( castlePos + new Vector3(xRandomPos, 0, yRandomPos));
            }
            else if (WorkTarget != null)
            {
                Vector3 unitPosition = WorkTarget.transform.position;
                UnitMovment.Move(unitPosition);
                
                if (UnitMovment.IsMovementStop() && _isDelay)
                {
                    StartCoroutine(Delay());
                }
            }
            else if (WorkTarget == null && WorkType == WorkType.ResourceMiner)
            {
                WorkTypeChangeNone();
            }

            BounceAmplitude();
        }

        private void BounceAmplitude()
        {
            if (!UnitMovment.IsMovementStop() && Visual != null)
            {
                float rawBounce = Mathf.Sin(Time.time * _bounceFrequency * Mathf.PI * 2f) * _bounceAmplitud;

                float bounce = rawBounce < 0f ? -rawBounce : rawBounce;

                Vector3 visualPosition = _initialLocalPosition + new Vector3(0f, bounce, 0f);
                Visual.transform.localPosition = visualPosition;
            }
            else if (Visual != null)
            {
                Visual.transform.localPosition = _initialLocalPosition;
            }
        }

        IEnumerator MoveDelay()
        {
            _isMoveDelay = false;
            yield return new WaitForSeconds(Random.Range(2, 10));

            if (WorkingUnitManager.Instance.MaxWorkingUnitCount < 50)
            {
                xRandomPos = UnityEngine.Random.Range(-5f, 5f);
                yRandomPos = UnityEngine.Random.Range(-5f, 5f);
            }
            else if (WorkingUnitManager.Instance.MaxWorkingUnitCount < 100)
            {
                xRandomPos = UnityEngine.Random.Range(-10f, 10f);
                yRandomPos = UnityEngine.Random.Range(-10f, 10f);
            }
            else if (WorkingUnitManager.Instance.MaxWorkingUnitCount > 150)
            {
                xRandomPos = UnityEngine.Random.Range(-15f, 15f);
                yRandomPos = UnityEngine.Random.Range(-15f, 15f);
            }
            
            
            _isMoveDelay = true;
        }

        IEnumerator Delay()
        {
            if (_isDelay == false) yield break;
            _isDelay = false;
            yield return new WaitForSeconds(_workSpeed);
            
            if (WorkType == WorkType.ResourceMiner)
            { 
                if(WorkTarget.TryGetComponent<IResource>(out var resource))
                {
                    if (resource != null)
                    {
                        resource.HitResource(_workPower);
                    }
                }
                else if(WorkTarget == null || WorkingUnitManager.Instance.ResourceList.Count <= 0)
                {
                    var resourceMiners = WorkingUnitManager.Instance.WorkPriority.FirstOrDefault
                        (w => w.workType == WorkType.ResourceMiner);

                    if (resourceMiners != null)
                    {
                        resourceMiners.maxWorkUnitCount -= 1;
                    }
                    
                    WorkTypeChangeNone();
                }
            }
            else if (WorkType == WorkType.Porter)
            {
                if (WorkTarget.GetComponent<DropItem>() != null)
                {
                    GameObject catchObject = Instantiate(WorkTarget, catchPostiton.position, catchPostiton.rotation);
                    catchObject.transform.SetParent(catchPostiton.transform);
                    ResoursTarget = catchObject;
                    Destroy(WorkTarget);
                    
                    float currentDistance = int.MaxValue;
                    
                    foreach (var building in WorkingUnitManager.Instance.ResourceBuilding)
                    {
                        float distance = Vector3.Distance(building.transform.position, transform.position);
                        
                        if (currentDistance > distance)
                        {
                            currentDistance = distance;
                            WorkTarget = building.gameObject;
                        }
                    }
                }
                else if (WorkTarget.GetComponent<BuildObject>() != null)
                {
                    ResourceType resource = ResoursTarget.GetComponent<DropItem>().DropItemType;
                    
                    if(resource == ResourceType.STONE)
                    {
                        ResourceManager.Instance.AddResorce(ResourceType.STONE, _stoneCollection);
                    }
                    else if (resource == ResourceType.WOOD)
                    {
                        ResourceManager.Instance.AddResorce(ResourceType.WOOD, _woodCollection);
                    }
                    else if (resource == ResourceType.CRYSTAL)
                    {
                        ResourceManager.Instance.AddResorce(ResourceType.CRYSTAL, _crystalCollection);
                    }
                    
                    var porters = WorkingUnitManager.Instance.WorkPriority.FirstOrDefault
                        (w => w.workType == WorkType.Porter);

                    if (porters != null)
                    {
                        porters.maxWorkUnitCount -= 1;
                    }

                    WorkTarget = null;
                    ResoursTarget = null;
                    Destroy(catchPostiton.GetChild(0).gameObject);
                    WorkTypeChangeNone();
                }

            }
            else if (WorkType == WorkType.Repairman)
            {
                 
            }
            else if (WorkType == WorkType.Farmer)
            {
                ResourceManager.Instance.AddResorce(ResourceType.FOOD, _foodCollection);

            }
            else if (WorkType == WorkType.Tavernkeeper)
            {
                
            }
            else if (WorkType == WorkType.Priest)
            {
            }

            if (UnitMovment.IsMovementStop() && IsReset && WorkType != WorkType.ResourceMiner 
                && WorkType != WorkType.Porter && WorkType != WorkType.None && WorkType != WorkType.Farmer && WorkType != WorkType.Priest)
            {
                if(Visual.activeSelf)
                    Instantiate(particleSystem, transform.position, Quaternion.identity).transform.SetParent(transform);
                
                Visual.SetActive(false);
                CheckOnCanWorkEvent();
                IsReset = false;
            }
            _isDelay = true;
        }
        
        private void CheckOnCanWorkEvent()
        {
            if(WorkTarget == null) return;
            
            BuildObject buildObject = WorkTarget.GetComponent<BuildObject>();
            
            print(buildObject.WorkingUnitList.Count);

            int units = buildObject.WorkingUnitList.Count;

            for (int i = 0; i < units; i++)
            {
                WorkingUnit buildings = buildObject.WorkingUnitList.Dequeue();
                
                if (buildings.Visual.activeSelf)
                {
                    buildObject.WorkingUnitList.Enqueue(buildings);
                    return;
                }
                
                buildObject.WorkingUnitList.Enqueue(buildings);
            }
            
            buildObject.OnCanWorkEvent?.Invoke();
        }

        private void ChangeWeater(WeatherType obj)
        {
            SettingUnitData();
            
            switch (obj)
            {
                case WeatherType.Sunny:
                {
                    break;
                }
                case WeatherType.LightRain:
                {
                    _woodCollection *= (int)(1 + _woodCollection * 0.05);
                    _foodCollection *= (int)(1 + _foodCollection * 0.05);
                    
                    break;
                }
                case WeatherType.MediumRain:
                {
                    _woodCollection *= (int)(1 + _woodCollection * 0.15);
                    _foodCollection *= (int)(1 + _foodCollection * 0.1);
                    
                    break;
                }
                case WeatherType.HeavyRain:
                {
                    //_woodCollection *= (int)(1 + _woodCollection * 0.15);
                    //_foodCollection *= (int)(1 + _foodCollection * 0.15);
                    
                    _workSpeed *= (int)(_workSpeed * 0.2);
                    _moveSpeed *= (int)(_moveSpeed * 0.3);
                    _workPower *= (int)(_workPower * 0.25);
                    
                    break;
                }
                case WeatherType.LightSnow:
                {
                    //_crystalCollection *= (int)(1 + _crystalCollection * 0.05);
                    //_stoneCollection *= (int)(1 + _stoneCollection * 0.05);
                    
                    break;
                }
                case WeatherType.MediumSnow:
                {
                    //_crystalCollection *= (int)(1 + _crystalCollection * 0.15);
                    //_stoneCollection *= (int)(1 + _stoneCollection * 0.1);
                    
                    _workSpeed *= (int)(_workSpeed * 0.25);
                    _moveSpeed *= (int)(_moveSpeed * 0.15);
                    _workPower *= (int)(_workPower * 0.05);
                    
                    break;
                }
                case WeatherType.HeavySnow:
                {
                    //_crystalCollection *= (int)(1 + _crystalCollection * 0.15);
                    //_stoneCollection *= (int)(1 + _stoneCollection * 0.15);
                    
                    _workSpeed *= (int)(_workSpeed * 0.3);
                    _moveSpeed *= (int)(_moveSpeed * 0.4);
                    _workPower *= (int)(_workPower * 0.3);
                    
                    break;
                }
                case WeatherType.RedRain:
                {
                    _workSpeed *= (int)(_workSpeed * 0.16);
                    _moveSpeed *= (int)(_moveSpeed * 0.16);
                    _workPower *= (int)(_workPower * 0.16);
                    _crystalCollection *= (int)(_crystalCollection * 0.16);
                    _stoneCollection *= (int)(_stoneCollection * 0.16);
                    _woodCollection *= (int)(_woodCollection * 0.16);
                    _foodCollection *= (int)(_foodCollection * 0.16);
                    
                    break;
                }
                case WeatherType.Dust:
                {
                    _workSpeed *= (int)(_workSpeed * 0.3);
                    _moveSpeed *= (int)(_moveSpeed * 0.3);
                    _workPower *= (int)(_workPower * 0.4);
                    
                    _crystalCollection *= (int)(_crystalCollection * 0.16);
                    _stoneCollection *= (int)(_stoneCollection * 0.16);
                    
                    break;
                }
            }
        }
        
        private void WorkTypeChangeNone()
        {
            WorkType = WorkType.None;
            ResoursTarget = null;
            IsReset = true;
            Visual.SetActive(true);
            WorkingUnitManager.Instance.UnemployedUnitList.Enqueue(this);
            WorkingUnitManager.Instance.SortUnitWorkPriority();
        }
    }
}