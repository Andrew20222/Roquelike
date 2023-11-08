using System.Collections.Generic;
using Abilities.Enums;
using UnityEngine;

namespace Abilities.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Abilities/BuffInfo", fileName = "BuffInfo", order = 0)]    
    public class BuffInfo : ScriptableObject
    {
        [field: SerializeField] public Ability Ability { get; private set; }
        [field: SerializeField] public List<Buff> CanBuffOffer { get; private set; }
    }
}