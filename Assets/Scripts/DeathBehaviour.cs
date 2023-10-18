using System;
using Enemy;
using Mana;
using Pools;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeathBehaviour : MonoBehaviour
{
    public event Action DeathEvent;
    public event Func<IManaPoolable<ManaFiller>> GetManaEvent;

    public void Death()
    {
        var position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        var count = Random.Range(10, 100);
        DeathEvent?.Invoke();
        var mana = GetManaEvent?.Invoke();
        
        if (mana != null)
        {
            Debug.Log("Mana");
            mana.SetCount(count);
            mana.SetPosition(position);
            mana.Play();
        }
        
        gameObject.SetActive(false);
    }
}