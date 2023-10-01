using DefaultNamespace;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private PlayerContainer playerContainer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ManaFiller mana))
        {
            playerContainer.ManaHandler.AddMana(mana.ManaCount);
            mana.PlayDestroyAnimation();
        }
    }
}
