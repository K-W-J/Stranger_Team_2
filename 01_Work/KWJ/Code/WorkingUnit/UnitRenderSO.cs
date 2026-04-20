using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.KWJ._01_Scripes.WorkingUnit
{
    [CreateAssetMenu(fileName = "WorkingUnitRenderSO", menuName = "SO/Unit/WorkingUnitRender", order = 0)]
    public class UnitRenderSO : ScriptableObject
    {
        public List<GameObject> UnitRenders = new List<GameObject>();
    }
}