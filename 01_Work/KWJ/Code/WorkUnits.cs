using System;
using System.Collections.Generic;
using _01_Work.HS.Building;
using _01_Work.KWJ._01_Scripes.WorkingUnit;
using UnityEngine;

namespace _01_Work.KWJ._01_Scripes
{
    [Serializable]
    public class WorkUnits
    {
        public WorkType workType; 
        
        public List<BuildObject> currentBuildObjects = new List<BuildObject>(); //건물 작업 유닛이면 이거 사용
        
        public int maxWorkUnitCount; //자원 작업 유닛이면 이거 사용
        public int currentWorkUnitCount;
        
        public Queue<WorkingUnit.WorkingUnit> currentWorkingUnits = new Queue<WorkingUnit.WorkingUnit>(); //자원 작업 유닛이면 이거 
        
        public GameObject priorityUI;
    }
}