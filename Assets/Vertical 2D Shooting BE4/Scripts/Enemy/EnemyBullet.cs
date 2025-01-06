using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed {  get; protected set; }
    private Player player;
    private Rigidbody2D rb;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var findGo = GameObject.FindWithTag("Player");
        player = findGo.GetComponent<Player>();
    }

    public virtual void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        rb.AddForce(direction * Speed, ForceMode2D.Force);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision == player.GetComponent<CircleCollider2D>())
        {
            Destroy(gameObject);
        }
    }
}
