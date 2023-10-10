using Interfaces;

namespace Pools
{
    public interface ISetStoppable
    {
        public void SetStoppable(IStopObservable stopObservable);
    }
}