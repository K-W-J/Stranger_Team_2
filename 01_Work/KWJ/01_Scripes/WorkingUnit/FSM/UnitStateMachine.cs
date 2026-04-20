using System;
using System.Collections.Generic;
using KWJ.Unit;
using UnityEngine;

namespace KWJ.FSM
{
    public class UnitStateMachine
    {
        [SerializeField] public UnitState CurrentState { get; set; }
        private Dictionary<string, UnitState> _states;

        public UnitStateMachine(Units units, StateSO[] stateList)
        {
            _states = new Dictionary<string, UnitState>();
            foreach (StateSO state in stateList)
            {
                Type type = Type.GetType(state.className);

                UnitState unitState = Activator.CreateInstance(type, units) as UnitState;

                unitState.InitStateMachine(units, this, units.UnitMovment);
                _states.Add(state.stateName, unitState);
            }

        }

        public void ChangeState(string newStateName, bool forced = false)
        {
            UnitState newState = _states.GetValueOrDefault(newStateName);

            if (forced == false && CurrentState == newState)
                return; //강제전환이 활성화되어 있지 않은 상태에서 현재 상태와 동일한 상태로 전환은 막는다.

            CurrentState?.Exit(); //이걸 아래로.
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public void UpdateStateMachine()
        {
            CurrentState?.Update();
        }
    }
}