using UnityEngine;
using UnityEngine.XR;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;
using static UnityEngine.UI.CanvasScaler;


namespace _01_Work.KHJ.CombatUnit
{
    public class FriendlyUnitCanAttackState : CombatUnitState
    {
        public FriendlyUnitCanAttackState(CombatUnit combatUnit, int animationHash) : base(combatUnit, animationHash)
        {
        }
        public override void Update()
        {
            base.Update();
            Collider[] hits = Physics.OverlapSphere(_combatUnit.transform.position, _combatUnit.data.FindRange, _combatUnit.whatIsUnit);
            foreach (Collider hit in hits)
            {
                if (hit.TryGetComponent(out CombatUnit target) && hit.transform != _combatUnit.transform &&
                    target.data.Team != _combatUnit.data.Team && target._isDeath == false)
                {
                    _combatUnit.ChangeState("CHASE");
                    break;
                }
            }
        }
    }
}