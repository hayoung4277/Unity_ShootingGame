using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyA : Enemy
{
    private float maxHp = 20;

    private float speed = 5f;

    private BulletSpawner bullet;
    private EnemySpawner spawner;
    private Animator animator;
    private Rigidbody rb;
    private GameManager gm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        var findBullet = GameObject.FindWithTag("Bullet");
        bullet = findBullet.GetComponent<BulletSpawner>();

        var findGo = GameObject.FindWithTag("EnemySpawner");
        spawner = findGo.GetComponent<EnemySpawner>();

        var findGm = GameObject.FindWithTag("GameController");
        gm = findGm.GetComponent<GameManager>();

        Hp = maxHp;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
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
        if(collision.CompareTag("Bullet"))
        {
            OnDamage(bullet.damage);
        }
        
        if(collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }

    public void Move()
    {
        StartCoroutine(MoveDiagonal());
    }

    private IEnumerator MoveDiagonal()
    {
        while(true)
        {
            if(spawner.spawnXPos < 0)
            {
                transform.Translate(new Vector3(1, -1, 0).normalized * speed * Time.deltaTime);
                yield return null;
            }
            else
            {
                transform.Translate(new Vector3(-1, -1, 0).normalized * speed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
