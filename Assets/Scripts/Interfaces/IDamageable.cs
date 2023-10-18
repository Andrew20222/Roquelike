namespace Interfaces
{
    public interface IDamageable
    {
        bool isAlive { get; }
        bool IsEnemy { get; }
        void TakeDamage(float damage);
    }
}