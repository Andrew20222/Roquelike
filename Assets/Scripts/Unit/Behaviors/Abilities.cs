using System;
using System.Collections.Generic;
using Abilities;
using Abilities.Enums;
using Abilities.ScriptableObjects;
using UnityEngine;
using Ability = Abilities.Enums.Ability;

namespace Unit.Behaviors
{
    public class Abilities : MonoBehaviour
    {
        [SerializeField] private List<AbilityInfo<BaseUpgraderInfo>> abilities;
        [SerializeField] private Transform spawnPoint;
        private Dictionary<Ability, AbilityBase<BaseUpgraderInfo>> _actualAbilities;
        private Dictionary<Ability, AbilityBase<BaseUpgraderInfo>> _untrueAbilities;
        private Action<Action<bool>> _stopSubscriber;

        private void Init()
        {
            _actualAbilities = new Dictionary<Ability, AbilityBase<BaseUpgraderInfo>>();
            _untrueAbilities = new Dictionary<Ability, AbilityBase<BaseUpgraderInfo>>();
            foreach (var ability in abilities)
            {
                var instance = Instantiate(ability.Prefab, spawnPoint);
                instance.Finish();
                instance.SetUpgrader(ability.UpgraderInfo);
                _stopSubscriber?.Invoke(instance.UpdateStop);
                _untrueAbilities.Add(ability.Ability, instance);
            }
        }

        public void SetAbility(Ability type)
        {
            if (_untrueAbilities.Remove(type, out AbilityBase<BaseUpgraderInfo> instance))
            {
                instance.Init();
                _actualAbilities.Add(type, instance);
            }
            else
            {
                Debug.LogError($"I cannot find ability type: {type.ToString()}");
            }
        }

        public void UpgradeAbility(Ability ability, Buff buff)
        {
            if (_actualAbilities.TryGetValue(ability, out AbilityBase<BaseUpgraderInfo> value))
            {
                value.Upgrade(buff);
            }
            else
            {
                Debug.LogError($"I cannot find ability type: {ability.ToString()}");
            }
        }

        public void SubscribeStop(Action<Action<bool>> subscribe)
        {
            _stopSubscriber = subscribe;
            Init();
        }
    }
}