using _01_Work.HS.Core.GameManagement;
using KWJ.FSM;
using UnityEngine;

namespace KWJ.Unit
{
    public class Units : SelectObject
    {
        [field: SerializeField] public UnitMovement UnitMovment { get; set; }
        
        [SerializeField] private StateSO[] _states;
        
        protected UnitStateMachine _stateMachine;

        protected override void Awake()
        {
            //_stateMachine = new UnitStateMachine(this, _states);
        }

        protected virtual void Start()
        {
            //_stateMachine.ChangeState("IDLE_WorkingUnit");
        }

        protected virtual void Update()
        {
            //_stateMachine.UpdateStateMachine();
            
        }
        
        //protected virtual void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}