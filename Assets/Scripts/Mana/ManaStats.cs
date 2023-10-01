using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ManaStats : MonoBehaviour, IManaHandler
    {
        public event Action<float, float> OnUpdateMana;
        public float CurrentValue { get; private set; }
        public float MaxValue { get; private set; }

        public void SetMaxValue(float maxValue)
        {
            MaxValue = maxValue;
        }

        public void AddMana(float value)
        {
            CurrentValue += value;
            OnUpdateMana?.Invoke(CurrentValue, MaxValue);
        }
    }
}