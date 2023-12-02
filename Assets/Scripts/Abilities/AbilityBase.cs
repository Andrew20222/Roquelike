using System.Collections.Generic;
using Abilities.Enums;
using Abilities.ScriptableObjects;
using Better.Attributes.Runtime;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityBase<TUpgader> : MonoBehaviour where TUpgader : BaseUpgraderInfo
    {
        [SerializeField] protected TUpgader upgader;
        protected Dictionary<Buff, int> _buffLevelViewers;
        protected Dictionary<Buff, List<float>> _buffLevelValues;
        protected bool _isStop;
        public void UpdateStop(bool value) => _isStop = value;
        [EditorButton]
        public abstract void Init();
        [EditorButton]
        public abstract void Finish();
        public abstract void Upgrade(Buff value);

        public void SetUpgrader(TUpgader value)
        {
            upgader = value;
            FillLevelsBuff();
        }

        protected virtual void FillLevelsBuff()
        {
            _buffLevelViewers ??= new Dictionary<Buff, int>();
            _buffLevelValues ??= new Dictionary<Buff, List<float>>();
            foreach (var viwer in upgader.Viwers)
            {
                _buffLevelValues.Add(viwer.Buff, viwer.Value);
                _buffLevelViewers.Add(viwer.Buff, 0);
            }
        }
    }
}