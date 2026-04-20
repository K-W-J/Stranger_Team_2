using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _01_Work.HS.Core;
using _01_Work.HS.Building;
using _01_Work.HS.Building.BuildingSO;
using _01_Work.LCM._01.Scripts.BuildResources.Resource;
using _01_Work.LCM._01.Scripts.BuildResources.Resource.EachResource;
using TMPro;

namespace _01_Work.KWJ._01_Scripes.WorkingUnit
{
    public class WorkingUnitManager : MonoSingleton<WorkingUnitManager>
    {
        [field:SerializeField] public List<WorkUnits> WorkPriority { get; private set; } = new List<WorkUnits>();
        public List<GameObject> ResourceBuilding { get; private set; } = new List<GameObject>();
        public Queue<WorkingUnit> UnemployedUnitList { get; set; } = new Queue<WorkingUnit>();
        
        public Queue<GameObject> ResourceList = new Queue<GameObject>();
        public BuildObject CastleBuilding { get; private set; }
        
        public Action OnMaxUnitChanged { get; set; }
        public int MaxWorkingUnitCount { get; private set; } 
        
        [SerializeField] private UnitRenderSO unitRenderSO;
        
        [SerializeField] private GameObject context;
        [SerializeField] private GameObject priorityUI;
        [SerializeField] private GameObject workingUnitPrefab;
        
        private void Update()
        {
            HandleResourceMining();
            
            HandleResourceTakeMining();
        }
        
        public void AddMaxWorkingUnit(int addWorkUnit, Transform spawnPoint)
        {
            MaxWorkingUnitCount += addWorkUnit;
            
            for (int i = 0; i < addWorkUnit; i++)
            {
                GameObject workingUnit = Instantiate(workingUnitPrefab, spawnPoint.position, Quaternion.identity);
                workingUnit.transform.SetParent(this.transform);
                UnemployedUnitList.Enqueue(workingUnit.GetComponent<WorkingUnit>());
            }
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
            
            OnMaxUnitChanged?.Invoke();
        }
        
        public void WorkPrioritySetting(BuildObject targetBuilding)
        {
            BuildingType buildingType = targetBuilding.BuildingDataSO.buildingType;
            
            if (buildingType == BuildingType.ResourceWarehouse)
            {
                ResourceBuilding.Add(targetBuilding.gameObject);
                return;
            }
            else if (buildingType == BuildingType.Castle)
            {
                CastleBuilding = targetBuilding;
                ResourceBuilding.Add(targetBuilding.gameObject);
                return;
            }
            
            if(targetBuilding.BuildingDataSO.personNum <= 0) return;
            
            WorkUnits workUnits = new WorkUnits();
            
            if (WorkPriority.Any(w => w.workType == BuildingTypeChangeWorkType(buildingType)))
            {
                WorkType targetType = BuildingTypeChangeWorkType(buildingType);
                
                for (int i = 0; i < WorkPriority.Count; i++)
                {
                    if (WorkPriority[i].workType == targetType)
                    {
                        WorkPriority[i].currentBuildObjects.Add(targetBuilding);
                        WorkPriority[i].maxWorkUnitCount += targetBuilding.BuildingDataSO.personNum;
                        SettingPriorityUI(WorkPriority[i]);
                    }
                }
            }
            else
            {
                workUnits.currentBuildObjects.Add(targetBuilding);
                workUnits.maxWorkUnitCount += targetBuilding.BuildingDataSO.personNum;
                workUnits.workType = BuildingTypeChangeWorkType(buildingType);

                InitWorkUnits(workUnits);
                
                WorkPriority.Insert(0, workUnits);
            }
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
        }

        private void InitWorkUnits(WorkUnits workUnits)
        {
            GameObject tempPriorityUI = Instantiate(priorityUI).gameObject;
            tempPriorityUI.transform.SetParent(context.transform);

            SwitchItemPos switchItemPos = tempPriorityUI.GetComponent<SwitchItemPos>();
            
            switchItemPos.Text.text = 
                $"{workUnits.workType} {0}/{workUnits.maxWorkUnitCount}";
            
            switchItemPos.worktype = workUnits.workType;
            tempPriorityUI.transform.SetSiblingIndex(0);
            workUnits.priorityUI = tempPriorityUI;
        }

        private void SettingPriorityUI(WorkUnits workUnits)
        {
            if(workUnits == null || workUnits.priorityUI == null) return;
            
            string workTypeString;
            
            switch (workUnits.workType)
            {
                case WorkType.ResourceMiner:
                {
                    workTypeString = "자원 채굴가";
                    break;
                }
                case WorkType.Porter:
                {  
                    workTypeString = "짐꾼";
                    break;
                }
                case WorkType.Archer:
                {
                    workTypeString = "궁수";
                    break;
                }
                case WorkType.Farmer:
                {
                    workTypeString = "농부";
                    break;
                }
                case WorkType.Repairman:
                {
                    workTypeString = "수리공";
                    break;
                }
                case WorkType.Tavernkeeper:
                {
                    workTypeString = "선술집 주인";
                    break;
                }
                case WorkType.Priest:
                {
                    workTypeString = "성직자";
                    break;
                }
                case WorkType.Parkranger:
                {
                    workTypeString = "광장 관리인";
                    break;
                }
                case WorkType.Warrior:
                {
                    workTypeString = "전사";
                    break;
                }
                case WorkType.Bankteller:
                {
                    workTypeString = "은행원";
                    break;
                }
                default:
                {
                    return;
                }
            }

            if (workUnits.workType == WorkType.Porter || workUnits.workType == WorkType.ResourceMiner)
            {
                workUnits.priorityUI.GetComponentInChildren<TextMeshProUGUI>().text = 
                    $"{workTypeString} {workUnits.currentWorkingUnits.Count}/{workUnits.maxWorkUnitCount}";
            }
            else
            {
                workUnits.priorityUI.GetComponentInChildren<TextMeshProUGUI>().text = 
                    $"{workTypeString} {workUnits.currentWorkUnitCount}/{workUnits.maxWorkUnitCount}";
            }
        }
 
        private void UnitRenderChange(WorkingUnit workingUnit)
        {
            Transform visual = workingUnit.Visual.transform;

            foreach (Transform child in visual)
            {
                Destroy(child.gameObject);
            }

            switch (workingUnit.WorkType)
            {
                case WorkType.Porter:
                {
                    Instantiate(unitRenderSO.UnitRenders[3], visual);
                    break;
                }
                case WorkType.ResourceMiner:
                {
                    if (workingUnit.WorkTarget == null) return;
                    
                    if (workingUnit.WorkTarget.GetComponent<CrystalResource>() != null ||
                        workingUnit.WorkTarget.GetComponent<RockResource>() != null)
                    {
                        Instantiate(unitRenderSO.UnitRenders[2], visual);
                    }
                    else if (workingUnit.WorkTarget.GetComponent<TreeResource>() != null)
                    {
                        Instantiate(unitRenderSO.UnitRenders[1], visual);
                    }

                    break;
                }
                case WorkType.Farmer:
                {
                    Instantiate(unitRenderSO.UnitRenders[4], visual);
                    break;
                }
                case WorkType.Priest:
                {
                    Instantiate(unitRenderSO.UnitRenders[5], visual);
                    break;
                }
                default:
                {
                    Instantiate(unitRenderSO.UnitRenders[0], visual);
                    break;
                }
            }
        }

        private WorkType BuildingTypeChangeWorkType(BuildingType buildingType) // BuildingType
        {
            switch (buildingType)
            {
                case BuildingType.Farm://농장
                {
                    return WorkType.Farmer;
                }
                case BuildingType.MasonryShop://석공소
                {
                    return WorkType.Repairman;
                }
                case BuildingType.Bar://술집
                {
                    return WorkType.Tavernkeeper;
                }
                case BuildingType.Church: //교회
                {
                    return WorkType.Priest;
                }
                case BuildingType.ResourceWarehouse: //자원저장소
                {
                    return WorkType.Porter;
                }
                case BuildingType.ArcherTower:
                {
                    return WorkType.Archer;
                }
                case BuildingType.Square:
                {
                    return WorkType.Parkranger;
                }
                case BuildingType.TrainingGround:
                {
                    return WorkType.Warrior;
                }
                case BuildingType.Bank:
                {
                    return WorkType.Bankteller;
                }
            }
            return WorkType.None;
        }

        public void SortUnitWorkPriority()
        {
            Queue<WorkingUnit> tempAllUnits = new Queue<WorkingUnit>();

            // 모든 유닛 가져오기
            foreach (var workUnit in WorkPriority)
            {
                if (workUnit.workType == WorkType.ResourceMiner || workUnit.workType == WorkType.Porter)
                {
                    int unitcount = workUnit.currentWorkingUnits.Count;
                        
                    for (int i = 0; i < unitcount; i++)
                    {
                        WorkingUnit workingUnit = workUnit.currentWorkingUnits.Dequeue();

                        if (workUnit.workType == WorkType.ResourceMiner && workingUnit.WorkTarget != null)
                        {
                            ResourceList.Enqueue(workingUnit.WorkTarget);
                            workingUnit.WorkTarget = null;
                            tempAllUnits.Enqueue(workingUnit);
                        }
                    }
                }
                else
                {
                    foreach (var currentBuildObject in workUnit.currentBuildObjects)
                    {
                        while (currentBuildObject.WorkingUnitList.Count > 0)
                        {
                            WorkingUnit workingUnit = currentBuildObject.WorkingUnitList.Dequeue();
                            tempAllUnits.Enqueue(workingUnit);
                        }
                    }
                }
            }

            // 백수 유닛도 포함
            while (UnemployedUnitList.Count > 0)
            {
                tempAllUnits.Enqueue(UnemployedUnitList.Dequeue());
            }
            
            // 유닛 다시 재분배
            foreach (var workUnit in WorkPriority) //직업 우선순위
            {
                workUnit.currentWorkUnitCount = 0;
                SettingPriorityUI(workUnit);
                
                if (workUnit.workType == WorkType.ResourceMiner && ResourceList.Count > 0) //만약 자원채굴가라면
                {
                    for (int i = 0; i < workUnit.maxWorkUnitCount && tempAllUnits.Count > 0 && ResourceList.Count > 0; i++)
                    {
                        WorkingUnit tempUnitCompo = tempAllUnits.Dequeue();
                        tempUnitCompo.WorkType = WorkType.ResourceMiner;
                        GameObject resourceObj = ResourceList.Dequeue();
                        
                        tempUnitCompo.IsReset = true;
                        
                        workUnit.currentWorkUnitCount += 1;
                        
                        tempUnitCompo.WorkTarget = resourceObj;
                        workUnit.currentWorkingUnits.Enqueue(tempUnitCompo);
                        UnitRenderChange(tempUnitCompo);
                    }
                    continue;
                }
                else if (workUnit.workType == WorkType.Porter && BuildResourceManager.Instance.takeResources.Count > 0)
                {
                    for (int i = 0; i < workUnit.maxWorkUnitCount && tempAllUnits.Count > 0 && BuildResourceManager.Instance.takeResources.Count > 0; i++)
                    {
                        WorkingUnit tempUnitCompo = tempAllUnits.Dequeue();
                        tempUnitCompo.WorkType = WorkType.Porter;
                        GameObject resourceObj = BuildResourceManager.Instance.takeResources.Dequeue();
                        ResourceType resourceType = resourceObj.GetComponent<DropItem>().DropItemType;
                        
                        if (ResourceManager.Instance.GetCurResource(resourceType) < ResourceManager.Instance.GetCurMaxResource(resourceType))
                        {
                            workUnit.currentWorkUnitCount += 1;
                            tempUnitCompo.IsReset = true;
                            tempUnitCompo.WorkTarget = resourceObj;
                        }
                        else
                        {
                            BuildResourceManager.Instance.takeResources.Enqueue(resourceObj);
                            tempUnitCompo.IsReset = true;
                            workUnit.currentWorkUnitCount -= 1;
                            
                            tempUnitCompo.WorkTarget = null;
                            tempUnitCompo.ResoursTarget = null;
                            tempUnitCompo.WorkType = WorkType.None;
                            UnitRenderChange(tempUnitCompo);
                            UnemployedUnitList.Enqueue(tempUnitCompo);
                            continue;
                        }
                         
                        workUnit.currentWorkingUnits.Enqueue(tempUnitCompo);
                        UnitRenderChange(tempUnitCompo);
                    }
                    continue;
                }
                
                foreach (var currentBuildObject in workUnit.currentBuildObjects) //한 건물 당 유닛 수
                {
                    while (!currentBuildObject.CheckCanWork() && tempAllUnits.Count > 0)
                    {
                        WorkingUnit tempUnitCompo = tempAllUnits.Dequeue();

                        if (tempUnitCompo.WorkType == WorkType.Porter ||
                            tempUnitCompo.WorkType == WorkType.ResourceMiner)
                        {
                            continue;
                        }
                        
                        workUnit.currentWorkUnitCount += 1;
                        SettingPriorityUI(workUnit);
                        
                        //만약 이전 직업과 다르다면 바꾸고 똑같으면 그대로 유지
                        if (tempUnitCompo.WorkType != workUnit.workType)
                        {
                            tempUnitCompo.IsReset = true;
                            tempUnitCompo.WorkTarget = currentBuildObject.gameObject;
                            tempUnitCompo.WorkType = workUnit.workType;

                            UnitRenderChange(tempUnitCompo);
                        }
                        
                        if(tempUnitCompo.WorkTarget != currentBuildObject.gameObject)
                            tempUnitCompo.WorkTarget = currentBuildObject.gameObject;
                        
                        currentBuildObject.AddPeople(tempUnitCompo);//주석 처리 진짜
                    }
                }
            }
            
            //찌끄레기들은 백수 리스트에 다시 넣기
            while (tempAllUnits.Count > 0)
            {
                WorkingUnit workingUnit = tempAllUnits.Dequeue();

                workingUnit.WorkType = WorkType.None;
                workingUnit.WorkTarget = null;

                UnitRenderChange(workingUnit);
                UnemployedUnitList.Enqueue(workingUnit);
                
            }
            
            OnMaxUnitChanged?.Invoke();
        }

        public void AddResources(List<Resource> resourceList)
        {
            if(resourceList == null || resourceList.Count <= 0) return;
            
            foreach (var resource in resourceList)
            {
                if(ResourceList.Count > 0 && ResourceList.Contains(resource.gameObject)) continue;
                
                resource.DragResource(); 
                ResourceList.Enqueue(resource.gameObject);
            }
            
            SettingResourceList();
        }
        
        public void SettingResourceList()
        {
            var resourceMiners = WorkPriority.FirstOrDefault(w => w.workType == WorkType.ResourceMiner);

            if (resourceMiners == null)
            {
                WorkUnits workUnitsTemp = new WorkUnits();

                workUnitsTemp.currentWorkingUnits = new Queue<WorkingUnit>();
                workUnitsTemp.maxWorkUnitCount = ResourceList.Count;
                workUnitsTemp.workType = WorkType.ResourceMiner;

                InitWorkUnits(workUnitsTemp);
                
                WorkPriority.Insert(0, workUnitsTemp);
            }
            else
            {
                resourceMiners.maxWorkUnitCount = ResourceList.Count;
            }
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
        }

        public void SettingTakeResourceList()
        {
            if (ResourceBuilding.Count <= 0 || ResourceBuilding == null) return;
            
            var porters = WorkPriority.FirstOrDefault(w => w.workType == WorkType.Porter);
            
            if (porters == null)
            {
                WorkUnits workUnitsTemp = new WorkUnits();

                workUnitsTemp.currentWorkingUnits = new Queue<WorkingUnit>();
                workUnitsTemp.maxWorkUnitCount += 1;
                workUnitsTemp.workType = WorkType.Porter;

                InitWorkUnits(workUnitsTemp);
                
                WorkPriority.Insert(0, workUnitsTemp);
                
                var resourceMiners = WorkPriority.FirstOrDefault(w => w.workType == WorkType.ResourceMiner);
                
                if (resourceMiners != null)
                {
                    int resourceMinerIndex = WorkPriority.IndexOf(resourceMiners);
                    workUnitsTemp.priorityUI.transform.SetSiblingIndex(resourceMinerIndex + 1);
                    WorkPriority.Insert(resourceMinerIndex + 1, workUnitsTemp);
                    SettingPriorityUI(workUnitsTemp);
                }
                else
                {
                    workUnitsTemp.priorityUI.transform.SetSiblingIndex(0);
                    SettingPriorityUI(workUnitsTemp);
                    WorkPriority.Insert(0, workUnitsTemp);
                }
            }
            else
            {
                porters.maxWorkUnitCount += 1;
            }
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
        }
        
        private void HandleResourceMining()
        {
            var resourceMiners = WorkPriority.FirstOrDefault(w => w.workType == WorkType.ResourceMiner);

            if (resourceMiners == null) return;

            if (resourceMiners.currentWorkingUnits.Count <= 0 && ResourceList.Count <= 0)
            {
                WorkPriority.Remove(resourceMiners);

                if(context.transform.childCount <= 0) return;

                foreach (Transform contexts in context.transform)
                {
                    if (contexts.GetComponent<SwitchItemPos>().worktype == WorkType.ResourceMiner)
                    {
                        Destroy(contexts.gameObject);
                    }
                }

                return;
            }

            if (resourceMiners.priorityUI != null)
            {
                SettingPriorityUI(resourceMiners);
                /*resourceMiners.priorityUI.transform.SetSiblingIndex(0);
                WorkPriority.Insert(0, resourceMiners);*/
            }
            
            int unitCount = resourceMiners.currentWorkingUnits.Count;

            for (int i = 0; i < unitCount && ResourceList.Count > 0; i++)
            {
                WorkingUnit tempUnitCompo = resourceMiners.currentWorkingUnits.Dequeue();

                if (tempUnitCompo.WorkTarget == null)
                {
                    tempUnitCompo.WorkTarget = ResourceList.Dequeue();
                }

                UnitRenderChange(tempUnitCompo);
                resourceMiners.currentWorkingUnits.Enqueue(tempUnitCompo);
            }
        }
        
        private void HandleResourceTakeMining()
        {
            var porters = WorkPriority.FirstOrDefault(w => w.workType == WorkType.Porter);

            if (porters == null) return;

            int takeResourcesCount = BuildResourceManager.Instance.takeResources.Count;

            if (porters.currentWorkingUnits.Count <= 0 && BuildResourceManager.Instance.takeResources.Count <= 0)
            {
                WorkPriority.Remove(porters);

                if(context.transform.childCount <= 0) return;

                foreach (Transform contexts in context.transform)
                {
                    if (contexts.GetComponent<SwitchItemPos>().worktype == WorkType.Porter)
                    {
                        Destroy(contexts.gameObject);
                    }
                }

                return;
            }
            
            if (porters.priorityUI != null)
            {
                SettingPriorityUI(porters);
            }

            int unitCount = porters.currentWorkingUnits.Count;

            for (int i = 0; i < unitCount; i++)
            {
                WorkingUnit tempUnitCompo = porters.currentWorkingUnits.Dequeue();

                if (tempUnitCompo.WorkTarget == null)
                {
                    if (tempUnitCompo.ResoursTarget == null && takeResourcesCount > 0)
                    {
                        tempUnitCompo.WorkTarget = BuildResourceManager.Instance.takeResources.Dequeue();
                    }
                }

                UnitRenderChange(tempUnitCompo);
                porters.currentWorkingUnits.Enqueue(tempUnitCompo);
            }
        }
        
        public void UpPriorityUI(WorkType workUnit)
        {
            var workUnits = WorkPriority.FirstOrDefault(w => w.workType == workUnit);
            
            int index = WorkPriority.IndexOf(workUnits);

            if(index <= 0 || index == -1) return;

            Swap(WorkPriority, index, --index);
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
            
        }

        public void DownPriorityUI(WorkType workUnit)
        {
            var workUnits = WorkPriority.FirstOrDefault(w => w.workType == workUnit);
            
            int index = WorkPriority.IndexOf(workUnits);
            
            if(index == -1 || index >= WorkPriority.Count - 1) return;
            
            Swap(WorkPriority, index, ++index);
            
            if(WorkPriority.Count > 0)
                SortUnitWorkPriority();
            
        }
        
        private void Swap(List<WorkUnits> list, int from, int to)
        {
            WorkUnits tmp = list[from];
            list[from] = list[to];
            list[to] = tmp;
        }
    }
}