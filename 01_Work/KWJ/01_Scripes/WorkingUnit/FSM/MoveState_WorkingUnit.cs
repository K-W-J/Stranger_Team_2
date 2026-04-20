using _01_Work.KWJ._01_Scripes.WorkingUnit;
using UnityEngine;

namespace KWJ.FSM
{
    public class MoveState_WorkingUnit : UnitState
    {
        protected WorkingUnit m_workingUnit;
        private Transform _targetPostiton;
        
        public override void Enter()
        {
            m_workingUnit = m_unit as WorkingUnit;
        }

        public override void Update()
        {
            base.Update();
            
            if (m_workingUnit.UnitMovment.IsMovementStop())
            {
                m_unitStateMachine.ChangeState("IDLE_WorkingUnit");
            }
        }

        public override void Exit()
        {

        }
    }
}
