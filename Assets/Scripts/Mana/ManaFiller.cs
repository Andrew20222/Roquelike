using UnityEngine;

namespace DefaultNamespace
{
    public class ManaFiller : MonoBehaviour
    {
        [field:SerializeField] public int ManaCount { get;private set; }

        public void PlayDestroyAnimation()
        {
            Destroy(gameObject);
        }
    }
}