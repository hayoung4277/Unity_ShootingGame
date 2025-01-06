using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyB : Enemy
{
    private float maxHp = 40;

    private float speed = 2f;

    private float hitTime = 0;
    private float invincibilityTime = 0.5f;

    private bool isHit;

    private BulletSpawner bullet;
    private Animator animator;
    private Rigidbody2D rb;
    private GameManager gm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        var findBullet = GameObject.FindWithTag("Bullet");
        bullet = findBullet.GetComponent<BulletSpawner>();

        var findGm = GameObject.FindWithTag("GameController");
        gm = findGm.GetComponent<GameManager>();

        Hp = maxHp;
        isHit = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Update()
    {
        hitTime += Time.deltaTime;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        isHit = true;
        animator.SetBool("Hit", isHit);
    }

    public override void OnDie()
    {
        base.OnDie();
        enabled = false;
        gm.AddScore();

        StartCoroutine(CoDieAnimation());
    }

    public IEnumerator CoDieAnimation()
    {
        animator.SetTrigger("Die");

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (!IsDead && hitTime > invincibilityTime)
            {
                OnDamage(bullet.damage);
                hitTime = 0;
                isHit = false;
            }
        }
    }

    public void Move()
    {
        rb.velocity = transform.up * -speed;
    }
}
