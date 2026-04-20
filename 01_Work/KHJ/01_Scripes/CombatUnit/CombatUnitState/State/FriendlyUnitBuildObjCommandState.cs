using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class FriendlyUnitCommendState : FriendlyUnitCanAttackState
    {
        public FriendlyUnitCommendState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.SetStop(false);
            _movement.SetDestination(_combatUnit.CommndPos);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (Vector3.Distance(_combatUnit.transform.position, _combatUnit.CommndPos) < 2)
            {
                _combatUnit.ChangeState("IDLE");
            }
        }
    }
}

