using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    private int playerHeart;
    private bool isDead;
    private bool isLeft;
    private bool isRight;
    private float playerHp = 100f;

    private float hitTime = 0;
    private float invincibilityTime = 0.5f;

    //public AudioClip sfxDeath;

    private Animator animator;
    private Rigidbody2D rb;
    private Boss boss;
    private EnemySpawner spawner;
    //private AudioSource audioSource;

    GameManager gm;

    private void Awake()
    {
        enabled = true;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>();

        var findGo = GameObject.FindWithTag("GameController");
        gm = findGo.GetComponent<GameManager>();

        var findSpawner = GameObject.FindWithTag("EnemySpawner");
        spawner = findSpawner.GetComponent<EnemySpawner>();

        isDead = false;
        playerHeart = 3;
    }

    private void Update()
    {
        hitTime += Time.deltaTime;

        if (isDead && playerHeart == 0)
            return;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        float hSpeed = h * speed;
        float vSpeed = v * speed;

        Vector2 velocity = new Vector2(hSpeed, vSpeed);
        rb.velocity = velocity;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isLeft = true;
            animator.SetBool("Left", isLeft);
        }
        else
        {
            isLeft = false;
            animator.SetBool("Left", isLeft);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;
            animator.SetBool("Right", isRight);
        }
        else
        {
            isRight = false;
            animator.SetBool("Right", isRight);
        }
    }

    private void OnDamage(float damage)
    {
        playerHp -= damage;

        if (playerHp < 0)
        {
            playerHp = 0;
            Dead();
        }
    }

    private void Dead()
    {
        animator.SetTrigger("Die");
        //audioSource.PlayOneShot(sfxDeath);
        this.enabled = false;

        gm.OnPlayerDie();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hitTime >= invincibilityTime)
        {
            if (collision.CompareTag("Boss"))
            {
                OnDamage(boss.damage);
            }

            if (collision.gameObject.tag == "Enemy")
            {
                OnDamage(20f);
            }

            if (collision.gameObject.tag == "EnemyBullet")
            {
                OnDamage(spawner.damage);
            }

            hitTime = 0f;
        }
    }
}
