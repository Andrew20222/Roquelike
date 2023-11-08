using System.Collections.Generic;
using UnityEngine;

namespace Abilities.ScriptableObjects.Infos.ProtectiveField
{
    [CreateAssetMenu(menuName = "Abilities/ProtectiveField/Upgrager", fileName = "Upgrager", order = 0)]
    public sealed class Upgrager : BaseUpgraderInfo
    {
        [field: SerializeField] public List<float> Radius { get; private set; }
    }
}