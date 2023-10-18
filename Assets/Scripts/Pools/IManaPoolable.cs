namespace Pools
{
    public interface IManaPoolable<T> : IPoolable<T>
    {
        public void SetCount(int count);
    }
}