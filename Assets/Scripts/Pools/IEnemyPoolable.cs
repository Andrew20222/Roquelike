using Unit.Player;

namespace Pools
{
    public interface IEnemyPoolable<T> : IPoolable<T>
    {
        public void Play(PlayerContainer playerContainer);
    }
}