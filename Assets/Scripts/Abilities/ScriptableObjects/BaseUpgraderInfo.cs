using System.Collections.Generic;
using UnityEngine;

namespace Abilities.ScriptableObjects
{
    public class BaseUpgraderInfo : ScriptableObject
    {
        [field: SerializeField] public List<float> Damage { get; protected set; }
        [field: SerializeField] public List<float> Cooldown { get; protected set; }
    }
}