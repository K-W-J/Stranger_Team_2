using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class CombatUnitHitState : CombatUnitState
    {
        private float _currentTime = 0;
        private float _stunTime = 0.5f;

        public CombatUnitHitState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.SetStop(true);
        }

        public override void Exit()
        {
            base.Exit();
            _movement.SetStop(false);
        }
        public override void Update()
        {
            base.Update();

            _currentTime += Time.deltaTime;
            if (_stunTime < _currentTime)
            {
                _currentTime = 0;
                _combatUnit.ChangeState("MOVE");
            }
        }
    }
}

