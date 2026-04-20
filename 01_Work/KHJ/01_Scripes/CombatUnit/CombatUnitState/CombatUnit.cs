using UnityEngine;
using _01_Work.HS.Core.GameManagement;
using _01_Work.HS.BuildingSystem.Building.Combat;
using _01_Work.HS.Building;
using System;

namespace _01_Work.KHJ.CombatUnit
{
    public abstract class CombatUnit : SelectObject, IHittable
    {
        [SerializeField] private HitEffect _hitEffect;
        [SerializeField] public CombatUnitDataSO data;
        [SerializeField] private CombatUnitStateDataSO[] states;
        [SerializeField] public LayerMask whatIsUnit;
        private EffectCreater _effectCreater;
        public CombatUnit _target { get; private set; }
        public IHittable _tower;

        public BuildObject TargetBuildObj;// 아군 유닛은 병영 넣으면 됨, 적군 유닛은 유저 성 넣으면 됨

        private CombatUnitStateMachine _stateMachine;
        private CombatUnitAnimatorTrigger _animTrigger;
        private UnitNavMovement _movement;

        public Vector3 MovePos { get; private set; }
        public Vector3 CommndPos { get; private set; }
        public bool _isDeath { get; private set; } = true;
        public float _hp { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new CombatUnitStateMachine(this, states);
            _animTrigger = GetComponentInChildren<CombatUnitAnimatorTrigger>();
            _movement = GetComponentInChildren<UnitNavMovement>();
            _effectCreater = GetComponentInChildren<EffectCreater>();
        }

        protected virtual void Start()
        {
            _movement.Initialize(this);
            _animTrigger.Initialize(this);
            ChangeState("IDLE");

            _movement.SetSpeed(data.MoveSpeed + UnityEngine.Random.Range(-0.15f, 0.15f));
            _hp = data.MaxHp;
        }

        protected void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void Hit(float damage, CombatUnit target)
        {
            _hitEffect.EffectPlay();
            Mathf.Clamp(_hp -= damage, 0, data.MaxHp);
            if (_hp <= 0)
            {
                //TargetBuildObj.DestroyUnit(this);
                _isDeath = true;
                ChangeState("DEATH");
                _hitEffect.DeathEffectPlay();
                AudioManager.Instance.PlaySfx("UNIT_DEATH");
            }
            else
            {
                ChangeState("HIT");
                AudioManager.Instance.PlaySfx("UNIT_HIT");
            }
        }

        public void Test()
        {
            _isDeath = false;
        }

        public void Death()
        {
            Destroy(gameObject);
        }


        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.AttackRange);
        }

        public void SetTarget(CombatUnit unit)
        {
            _target = unit;
        }

        public void Attack(Transform target)
        {
            if (target == null) return;
            if (_effectCreater != null)
            {
                if (data.AttackType == AttackType.Range)
                    AudioManager.Instance.PlaySfx("UNIT_ARROW");
                else
                    AudioManager.Instance.PlaySfx("UNIT_SWORD");
                _effectCreater.PlayEffect(target.transform);
            }
        }

        public void SetTargetBuildObj(BuildObject tower)
        {
            TargetBuildObj = tower;
        }

        public string GetState() => _stateMachine.GetCurrentStateName();
        public void SetMovePos(Vector3 pos) => MovePos = pos;
    }
}