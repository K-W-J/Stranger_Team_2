using System;
using System.Collections.Generic;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class CombatUnitStateMachine
    {
        public CombatUnitState CurrentState { get; private set; }
        private Dictionary<string, CombatUnitState> _states;

        public CombatUnitStateMachine(CombatUnit combatUnit, CombatUnitStateDataSO[] stateList)
        {
            _states = new Dictionary<string, CombatUnitState>();
            foreach (CombatUnitStateDataSO state in stateList)
            {
                Type type = Type.GetType(state.className);
                Debug.Assert(type != null, $"Finding type is null : {state.className}");
                CombatUnitState entityState = Activator.CreateInstance(type, combatUnit, state.animationHash)
                                        as CombatUnitState;
                _states.Add(state.stateName, entityState);
            }
        }

        public void ChangeState(string newStateName, bool forced = false)
        {
            CombatUnitState newState = _states.GetValueOrDefault(newStateName);
            Debug.Assert(newState != null, $"State is null {newStateName}");

            if (!forced && CurrentState == newState)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public string GetCurrentStateName()
        {
            foreach (var state in _states)
            {
                if (state.Value == CurrentState)
                    return state.Key;
            }
            return null;
        }

        public void UpdateStateMachine()
        {
            CurrentState?.Update();
        }
    }
}
