using _01_Work.KHJ.CombatUnit;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


namespace _01_Work.KHJ.CombatUnit
{

    public abstract class CombatUnitState
    {
        protected CombatUnit _combatUnit;
        protected int _animationHash;
        protected CombatUnitAnimator _combatUnitAnimator;
        protected CombatUnitAnimatorTrigger _animatorTrigger;
        protected bool _isTriggerCall;
        protected bool _isAttackCall;
        protected bool _isCool;

        protected readonly float _inputThreshold = 0.1f;
        protected UnitNavMovement _movement;


        protected CombatUnitState(CombatUnit combatUnit, int animationHash)
        {
            _combatUnit = combatUnit;
            _animationHash = animationHash;
            _combatUnitAnimator = combatUnit.GetComponentInChildren<CombatUnitAnimator>();
            _animatorTrigger = combatUnit.GetComponentInChildren<CombatUnitAnimatorTrigger>();
            _movement = _combatUnit.GetComponentInChildren<UnitNavMovement>();
        }


        public virtual void Enter()
        {
            _combatUnitAnimator.SetParam(_animationHash, true);
            _isTriggerCall = false;
            _animatorTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
            _animatorTrigger.OnAttackAnimationTrigger += AttackAnimationTrigger;
        }

        public virtual void Update() { }

        public virtual void Exit()
        {
            _combatUnitAnimator.SetParam(_animationHash, false);
            _animatorTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
            _animatorTrigger.OnAttackAnimationTrigger -= AttackAnimationTrigger;
        }

        public virtual void AnimationEndTrigger() => _isTriggerCall = true;
        public virtual void AttackAnimationTrigger() => _isAttackCall = true;
    }
}