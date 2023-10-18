using Mana;
using Pools;
using UnityEngine;

namespace Unit.Player.Magnet
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] private PlayerContainer playerContainer;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPoolable<ManaFiller> mana))
            {
                playerContainer.ManaHandler.AddMana(1);
                mana.Stop();
            }
        }
    }
}
