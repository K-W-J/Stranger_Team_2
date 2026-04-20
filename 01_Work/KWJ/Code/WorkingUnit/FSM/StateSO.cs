using UnityEngine;

namespace KWJ.Unit
{
    [CreateAssetMenu(fileName = "StateData", menuName = "SO/FSM/StateData", order = 0)]
    public class StateSO : ScriptableObject
    {
        public string stateName;
        public string className;

    }
}