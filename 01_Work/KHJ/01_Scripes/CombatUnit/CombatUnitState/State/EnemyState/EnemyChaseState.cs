using _01_Work.KHJ.CombatUnit;
using UnityEngine;


namespace _01_Work.KHJ.CombatUnit
{
    public class EnemyChaseState : CombatUnitState
    {


        public EnemyChaseState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.SetStop(false);
        }

        public override void Update()
        {
            base.Update();

            Collider[] hits = Physics.OverlapSphere(_combatUnit.transform.position, _combatUnit.data.FindRange, _combatUnit.whatIsUnit);


            float distance = float.MaxValue;
            CombatUnit _unit = null;

            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out CombatUnit unit) && unit != _combatUnit &&
                    unit.data.Team != _combatUnit.data.Team && unit._isDeath == false)
                {
                    float d = Vector3.Distance(unit.transform.position, _combatUnit.transform.position);
                    if (d < distance)
                    {
                        distance = d;
                        _unit = unit;
                    }
                }
            }

           

            if (_unit != null)
            {
                _combatUnit.SetTarget(_unit);
                if (Vector3.Distance(_combatUnit._target.transform.position, _combatUnit.transform.position) < _combatUnit.data.AttackRange)
                    _combatUnit.ChangeState("ATTACK");
                else
                    _movement.SetDestination(_combatUnit._target.transform.position);
            }
            else
            {
                if (Vector3.Distance(_combatUnit.TargetBuildObj.transform.position, _combatUnit.transform.position) < Mathf.Clamp(_combatUnit.data.AttackRange, 0.8f, 10))
                    _combatUnit.ChangeState("TOWER_ATTACK");
                else
                    _combatUnit.ChangeState("MOVE");
            }
        }
    }
}
