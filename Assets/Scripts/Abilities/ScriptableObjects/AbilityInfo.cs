using UnityEngine;

namespace Abilities.ScriptableObjects
{
    public class AbilityInfo<TUpgrager> : ScriptableObject where TUpgrager : BaseUpgraderInfo
    {
        [field: SerializeField] public Enums.Ability Ability { get; protected set; }
        [field: SerializeField] public GameObject Prefab { get; protected set; }
        [SerializeField] private TUpgrager upgraderInfo;
    }
}