using _01_Work.KHJ;
using _01_Work.KHJ.CombatUnit;
using UnityEngine;

public class EnemyMoveState : CombatUnitState
{
    public EnemyMoveState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
    {
    }

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
        _movement.SetDestination(_combatUnit.TargetBuildObj.transform.position);

        Collider[] hits = Physics.OverlapSphere(_combatUnit.transform.position, _combatUnit.data.FindRange, _combatUnit.whatIsUnit);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out CombatUnit unit) &&
                unit.data.Team != _combatUnit.data.Team && unit._isDeath == false)
            {
                _combatUnit.ChangeState("CHASE");
            }
            else if (Vector3.Distance(_combatUnit.TargetBuildObj.transform.position, _combatUnit.transform.position) < 1)
            {
                _combatUnit.ChangeState("CHASE");
                break;
            }
        }
    }
}

