using System;
using System.Collections.Generic;
using _01_Work.HS.Building;
using _01_Work.HS.Building.BuildingSO;
using _01_Work.KWJ._01_Scripes.WorkingUnit;
using UnityEngine;

namespace _01_Work.KWJ._01_Scripes.test
{
    public class test_building : MonoBehaviour
    {
        public BuildingDataSO BuildingDataSO;
        public Queue<WorkingUnit.WorkingUnit> WorkingUnitList = new Queue<WorkingUnit.WorkingUnit>(); 
        private void Start()
        {
            //WorkingUnitManager.Instance.WorkPrioritySetting(this);
        }

        private void Update()
        {
            //print(gameObject.name + " : " + WorkingUnitList.Count);
        }

        public void AddPeople(WorkingUnit.WorkingUnit workingUnit) => WorkingUnitList.Enqueue(workingUnit);
        public bool CheckCanWork() => WorkingUnitList.Count < BuildingDataSO.personNum;
    }
}