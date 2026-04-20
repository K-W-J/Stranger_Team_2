
using KWJ.Unit;
using UnityEngine;

namespace KWJ.FSM
{
    public abstract class UnitState : MonoBehaviour
    {
        protected UnitStateMachine m_unitStateMachine;
        protected UnitMovement m_movement;
        protected Units m_unit;
        
        public virtual void Enter()
        {

        }

        public virtual void Update()
        {
            
        }

        public virtual void Exit()
        {

        }

        public void InitStateMachine(Units units, UnitStateMachine unitStateMachine, UnitMovement movement)
        {
            m_unit = units;
            m_unitStateMachine = unitStateMachine;
            m_movement = movement;
            
        }
    }
}