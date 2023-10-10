using Enemy;

namespace Pools
{
    public interface ICanvasPoolable<T> : IPoolable<T>
    {
        public void Play(EnemyContainer enemyContainer);
    }
}