using System;
using UnityEngine;

namespace Mana
{
    public class ManaStats : MonoBehaviour, IManaHandler
    {
        public event Action<float, float> OnUpdateMana;
        public float CurrentValue { get; private set; }
        public float MaxValue { get; private set; }

        public void SetMaxValue(float maxValue)
        {
            MaxValue = maxValue;
            OnUpdateMana?.Invoke(CurrentValue, MaxValue);
        }

        public void AddMana(float value)
        {
            CurrentValue += value;
            OnUpdateMana?.Invoke(CurrentValue, MaxValue);
        }

        public void ReplishMana(float value)
        {
            CurrentValue -= value;
            OnUpdateMana?.Invoke(CurrentValue, MaxValue);
        }

        public void RemoveAllMana()
        {
            CurrentValue = 0;
            OnUpdateMana?.Invoke(CurrentValue, MaxValue);
        }
    }
}