using UnityEngine;

namespace Works.KWJ.WorkingUnit
{

    [CreateAssetMenu(fileName = "WorkingUnitSO", menuName = "SO/Unit/WorkingUnit", order = 0)]
    public class WorkingUnitDataSO : ScriptableObject
    {
        [Header("WorkState")]
        
        public float WorkSpeed;
        public int WorkPower;
        
        [Header("MovementState")]
        
        public float MoveSpeed;
        public float BounceAmplitude;
        public float BounceFrequency;
        
        [Header("ResourceCollection")]
        
        public int StoneCollection;
        public int CrystalCollection;
        public int WoodCollection;
        public int FoodCollection;   
    }
}