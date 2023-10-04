using System;
using Mana;
using Pools;
using UnityEngine;

public class DeathBehaviour : MonoBehaviour
{
    public event Action DeathEvent;
    public event Func<IPoolable<ManaFiller>> GetManaEvent;

    public void Death()
    {
        var position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        DeathEvent?.Invoke();
        var mana = GetManaEvent?.Invoke();
        if (mana != null)
        {
            mana.SetPosition(position);
            mana.Play();
        }

        //Set Mana count 
        Destroy(gameObject);
    }
}