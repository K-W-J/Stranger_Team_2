using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{
    public class CombatUnitAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private CombatUnit _combatUnit;

        public void Initialize(CombatUnit combatUnit)
        {
            _combatUnit = combatUnit;
        }

        public void SetParam(int hash, float value) => animator.SetFloat(hash, value);
        public void SetParam(int hash, int value) => animator.SetInteger(hash, value);
        public void SetParam(int hash, bool value) => animator.SetBool(hash, value);
        public void SetParam(int hash) => animator.SetTrigger(hash);
    }
}
