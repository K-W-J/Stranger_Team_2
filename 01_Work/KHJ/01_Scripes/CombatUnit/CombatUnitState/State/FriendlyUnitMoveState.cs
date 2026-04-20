using _01_Work.KHJ.CombatUnit;
using UnityEngine;


namespace _01_Work.KHJ.CombatUnit
{
    public class FriendlyUnitMoveState : FriendlyUnitCanAttackState
    {
        public FriendlyUnitMoveState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        private Vector3 _movePos = Vector3.zero;

        public override void Enter()
        {
            base.Enter();
            _combatUnit.Test();
            _movement.SetStop(false);
        }

        

        public override void Exit()
        {
            base.Exit();
            _movement.SetStop(true);
        }

        public override void Update()
        {
            base.Update();
            _movement.SetDestination(_combatUnit.MovePos);
            if (_movement.IsArrived)
            {
                _combatUnit.ChangeState("IDLE");
            }
        }
    }
}