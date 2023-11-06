using System;

namespace Mana
{
    public interface IManaHandler
    {
        event Action<float, float> OnUpdateMana;
        float CurrentValue { get;}
        float MaxValue { get;}
        void AddMana(float value);
        void ReplishMana(float value);

    }
}