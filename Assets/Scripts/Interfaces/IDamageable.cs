namespace Interfaces
{
    public interface IDamageable
    {
        bool IsEnemy { get; }
        void TakeDamage(float damage);
    }
}