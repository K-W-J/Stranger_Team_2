using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class EnemyAttackState : CombatUnitState
    {
        private float _currentTime = 0;
        private bool test = false;
        public EnemyAttackState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.SetStop(true);
            _combatUnit.transform.LookAt(_combatUnit._target.transform.position);
        }

        public override void Exit()
        {
            base.Exit();
            _isCool = false;
            _currentTime = 0;
            test = false;
        }

        public override void Update()
        {
            base.Update();


            if (_isTriggerCall)
            {
                if (test == false)
                {
                    test = true;
                    _combatUnit.Attack(_combatUnit._target.transform);
                    _combatUnit._target.Hit(_combatUnit.data.Damage, _combatUnit);
                }
                CoolTime();
                if (_isCool)
                {
                    _combatUnit.ChangeState("MOVE");
                }
            }
        }


        private void CoolTime()
        {
            if (_currentTime > _combatUnit.data.CoolTime)
            {
                _isCool = true;
            }
            else
            {
                _currentTime += Time.deltaTime;
            }
        }
    }
}
