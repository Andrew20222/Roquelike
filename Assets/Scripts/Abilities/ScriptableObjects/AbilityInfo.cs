using UnityEngine;

namespace Abilities.ScriptableObjects
{
    public class AbilityInfo<TUpgrager> : ScriptableObject where TUpgrager : BaseUpgraderInfo
    {
        [field: SerializeField] public Enums.Ability Ability { get; protected set; }
        [field: SerializeField] public AbilityBase<BaseUpgraderInfo> Prefab { get; protected set; }
        [field: SerializeField] public TUpgrager UpgraderInfo { get; private set; }
    }
}