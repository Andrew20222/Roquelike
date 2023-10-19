using Mana;
using Pools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unit.Player.Magnet
{
    public class Magnet : MonoBehaviour
    {
        [FormerlySerializedAs("playerContainer")] [SerializeField] private Container container;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPoolable<ManaFiller> mana))
            {
                container.ManaHandler.AddMana(1);
                mana.Stop();
            }
        }
    }
}
