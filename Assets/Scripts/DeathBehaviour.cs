using Mana;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    [SerializeField] private ManaFiller manaPrefab;

    public void Death()
    {
        var position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        Instantiate(manaPrefab, position, Quaternion.identity);
        //Set Mana count 
        Destroy(gameObject);
    }
}