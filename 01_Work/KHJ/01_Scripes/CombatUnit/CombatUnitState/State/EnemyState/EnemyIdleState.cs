using _01_Work.KHJ;
using _01_Work.KHJ.CombatUnit;
using UnityEngine;

public class EnemyIdleState : CombatUnitState
{
    public EnemyIdleState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
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
        _movement.SetStop(true);
    }

    public override void Update()
    {
        base.Update();
        if (_combatUnit.TargetBuildObj != null)
        {
            _combatUnit.ChangeState("MOVE");
        }
    }
}

