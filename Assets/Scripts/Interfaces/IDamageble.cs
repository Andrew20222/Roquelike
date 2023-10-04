namespace DefaultNamespace
{
    public interface IDamageble
    {
        bool IsEnemy { get; }
        void TakeDamage(float damage);
    }
}