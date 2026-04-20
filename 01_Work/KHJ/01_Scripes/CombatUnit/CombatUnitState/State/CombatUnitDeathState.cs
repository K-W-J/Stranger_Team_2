using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class CombatUnitDeathState : CombatUnitState
    {
        private float _currentTime = 0;
        private float _stunTime = 4;

        public CombatUnitDeathState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.SetStop(true);
            _movement.agent.enabled = false;
        }

        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
            {
                _combatUnit.transform.position += Vector3.down * 0.03f * Time.deltaTime;
                _currentTime += Time.deltaTime;
                if (_stunTime < _currentTime)
                {
                    _combatUnit.Death();
                }
            }
        }
    }
}