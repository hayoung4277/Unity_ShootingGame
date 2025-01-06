using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        var findGo = GameObject.FindWithTag("Player");
        player = findGo.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == player.GetComponent<CircleCollider2D>())
        {
            Destroy(gameObject);
        }

        if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
