using System;
using System.Collections.Generic;
using Abilities.Enums;
using UnityEngine;

namespace Abilities.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Abilities/BaseUpgrader", fileName = "Upgrader", order = 0)]
    public class BaseUpgraderInfo : ScriptableObject
    {
        [field: SerializeField] public List<BuffViwer> Viwers { get; protected set; }
    }

    [Serializable]
    public class BuffViwer
    {
        [field: SerializeField] public Buff Buff { get; private set; }
        [field: SerializeField] public List<float> Value { get; private set; }
    }
}