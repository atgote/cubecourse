// 2021.03.20 Tihonovschi Andrei
// Interface for destructible objects

// Available Damage types
public enum DamageType { VIRTUAL = 0, PHYSIC, FIRE, ELECTRIC};

// IDestructible interface
public interface IDestructible
{
    // Generic damage receiver
    public void Damage(float amount);

    // Specific damage receiver
    public void Damage(float amount, DamageType type);
}