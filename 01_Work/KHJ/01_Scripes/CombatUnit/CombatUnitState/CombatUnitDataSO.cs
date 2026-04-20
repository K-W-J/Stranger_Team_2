using UnityEngine;

namespace _01_Work.KHJ.CombatUnit
{

    [CreateAssetMenu(fileName = "CombatUnitDataSO", menuName = "SO/Unit/CombatUnitDataSO")]
    public class CombatUnitDataSO : ScriptableObject
    {
        [Header("UIData")]
        public string Name;
        public string ToolTip;

        [Header("Stat")]

        public float MaxHp;
        public float AttackRange;
        public float FindRange;
        public float MoveSpeed;
        public float CoolTime;
        public float Damage;
        public UnitTeam Team;
        public AttackType AttackType;
    }

    public enum UnitTeam
    {
        FriendlyUnit,
        EnemyUnit
    }

    public enum AttackType
    {
        Range,
        Melee,
    }
}