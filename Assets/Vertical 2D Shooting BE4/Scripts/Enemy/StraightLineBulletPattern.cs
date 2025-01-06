using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineBulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Boss boss;

    public float speed = 3f;
    public int bulletCount = 2;
    public float fireRate = 0.5f;
    private float fireInterval = 3f;

    private void Awake()
    {
        var findGo = GameObject.FindWithTag("Boss");
        boss = findGo.GetComponent<Boss>();

        enabled = false;
    }

    public void OnEnable()
    {
        enabled = true;
    }

    private void Start()
    {
        StartCoroutine(StraightLinePattern());
    }

    private void Update()
    {
        if (boss.isDead)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator StraightLinePattern()
    {
        while (true)
        {
            FireBullet();

            yield return new WaitForSeconds(fireRate);

            FireBullet();

            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void FireBullet()
    {
        float startPosX = 0.25f;
        float startPosY = -0.5f;

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.up * -speed;

            bullet.transform.Translate(startPosX, startPosY, 0f);

            startPosX -= 0.5f;
        }
    }
}
