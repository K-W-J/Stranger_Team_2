using _01_Work.KHJ.CombatUnit;

public interface IHittable
{
    public void Hit(float damage, CombatUnit combatUnit);
    public void Death();

    
}
