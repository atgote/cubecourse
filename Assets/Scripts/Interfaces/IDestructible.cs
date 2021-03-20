// Damage types
public enum DamageType { VIRTUAL = 0, PHYSIC, FIRE, ELECTRIC};

// IDestructible interface
public interface IDestructible
{
    public void Damage(float amount);
    public void Damage(float amount, DamageType type);
}