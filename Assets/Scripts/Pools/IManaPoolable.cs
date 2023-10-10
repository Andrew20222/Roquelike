namespace Pools
{
    public interface IManaPoolable<T> : IPoolable<T>
    {
        public void Play();
        public void SetCount(int count);
    }
}