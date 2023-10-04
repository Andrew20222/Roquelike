using System;

namespace Mana
{
    public interface IManaHandler
    {
        float CurrentValue { get;}
        float MaxValue { get;}
        void AddMana(float value);
        event Action<float, float> OnUpdateMana;
    }
}