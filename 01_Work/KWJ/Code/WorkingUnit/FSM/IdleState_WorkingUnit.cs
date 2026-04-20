using _01_Work.KWJ._01_Scripes.WorkingUnit;

namespace KWJ.FSM
{
    public class IdleState_WorkingUnit : UnitState
    {
        protected WorkingUnit m_workingUnit;
        
        public override void Enter()
        {
            m_workingUnit = m_unit as WorkingUnit;
            //Debug.Log(m_unit == null);
        }

        public override void Update()
        {
            base.Update();
            if (!m_workingUnit.UnitMovment.IsMovementStop())
            {
                m_unitStateMachine.ChangeState("MOVE_WorkingUnit");
            }
        }

        public override void Exit()
        {

        }
    }
}
