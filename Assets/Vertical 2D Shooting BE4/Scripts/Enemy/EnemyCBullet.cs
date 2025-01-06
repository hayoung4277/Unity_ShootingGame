using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCBullet : EnemyBullet
{
    private float bulletSpeed = 1f;

    public override void Awake()
    {
        base.Awake();
        Speed = bulletSpeed;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
