using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float stopPos = 2.8f;
    private float speed = 2.5f;

    public float hp;
    private float maxHp = 1000f;

    public float damage = 20f;

    private float hitTime = 0;
    private float invincibilityTime = 0.5f;

    private bool hitable;
    private bool isHit;
    public bool isDead;

    private Rigidbody2D rb;

    private GameManager gm;
    private BulletSpawner bullet;
    private Animator animator;
    private StraightLineBulletPattern straight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        var findGo = GameObject.FindWithTag("GameController");
        var findBullet = GameObject.FindWithTag("Bullet");
        gm = findGo.GetComponent<GameManager>();
        bullet = findBullet.GetComponent<BulletSpawner>();
        straight = GetComponent<StraightLineBulletPattern>();

        enabled = false;
        isHit = false;
        isDead = false;
        hitable = false;
        hp = maxHp;
    }

    private void Update()
    {
        if(isDead)
            return;

        hitTime += Time.deltaTime;

        rb.velocity = transform.up * -speed;

        var pos = transform.position;

        if(pos.y <= stopPos)
        {
            rb.velocity = new Vector2(0f, 0f);
            hitable = true;
            straight.OnEnable();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            if(!isDead && hitable && hitTime >= invincibilityTime)
            {
                OnDamage(bullet.damage);
                hitTime = 0f;
                isHit = false;
            }
        }
    }

    public void SpawnBoss()
    {
        enabled = true;   
    }

    public void Die()
    {
        hp = 0;
        isDead = true;     
        enabled = false;

        StartCoroutine(CoDieAnimation());
    }

    public IEnumerator CoDieAnimation()
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        gm.ClearGame();
    }

    public void OnDamage(float damage)
    {
        hp -= damage;
        isHit = true;
        animator.SetBool("Hit", isHit);
        if(hp <= 0f)
        {
            Die();
        }
    }
}
