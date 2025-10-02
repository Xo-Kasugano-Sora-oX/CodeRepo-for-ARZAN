using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERHP : MonoBehaviour
{
    [SerializeField] protected float MaxHP;
    protected float HP;

    protected virtual void OnEnable()
    {
        HP = MaxHP;
    }
    public virtual void TakeDamage(float damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            Die();
        } 
    }

    public virtual void Die()
    {

    }
}

