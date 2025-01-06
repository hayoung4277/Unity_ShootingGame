using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp { get; protected set; }
    public bool IsDead { get; private set; }

    protected virtual void OnEnable()
    {
        IsDead = false;
    }

    public virtual void OnDamage(float damage)
    {
        Hp -= damage;

        if(Hp <= 0)
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        Hp = 0;
        IsDead = true;
    }
}
