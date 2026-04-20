using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class FriendlyUnitIdleState : FriendlyUnitCanAttackState
    {


        public FriendlyUnitIdleState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
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
            if (_combatUnit.TargetBuildObj == null) return;
            if (Vector3.Distance(_combatUnit.MovePos, _combatUnit.transform.position) > 0.3f)
            {
                _combatUnit.ChangeState("MOVE");
            }
        }
    }
}