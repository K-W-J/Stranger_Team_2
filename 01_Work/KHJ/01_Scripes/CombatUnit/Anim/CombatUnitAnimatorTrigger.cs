using System;
using _01_Work.KHJ.CombatUnit;
using UnityEngine;

namespace _01_Work.KHJ.CombatUnit 
{
    public class CombatUnitAnimatorTrigger : MonoBehaviour
    {
        public Action OnAnimationEndTrigger;
        public Action OnAttackAnimationTrigger;
        public Action<bool> OnRollingStatusChange;
        public Action OnAttackVFXTrigger;
        public Action<bool> OnManualRotationTrigger;
        
        private CombatUnit _combatUnit;

        public void Initialize(CombatUnit combatUnit)
        {
            _combatUnit = combatUnit;
        }

        private void AnimationEnd() //매서드 명 오타나면 안된다. (이벤트 이름과 동일하게 만들어야 해.)
        {
            OnAnimationEndTrigger?.Invoke();
        }


        private void AttackStart() => OnAttackAnimationTrigger?.Invoke();
        private void RollingStart() => OnRollingStatusChange?.Invoke(true);
        private void RollingEnd() => OnRollingStatusChange?.Invoke(false);
        private void PlayAttackVFX() => OnAttackVFXTrigger?.Invoke();
        
        private void StartManualRotation() => OnManualRotationTrigger?.Invoke(true);
        private void StopManualRotation() => OnManualRotationTrigger?.Invoke(false);
    }
}