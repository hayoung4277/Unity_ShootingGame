using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Boss boss;

    private float startTime = 5;
    public int bulletCount = 12;    // 탄알 개수
    public float bulletSpeed = 3f;  // 탄알 속도
    public float fiteRate = 1f;
    public float fireInterval = 7f; // 발사 간격

    private void Awake()
    {
        var findGo = GameObject.FindWithTag("Boss");
        boss = findGo.GetComponent<Boss>();
    }

    private void Start()
    {
        StartCoroutine(FireCircularPattern());
    }

    private void Update()
    {
        if (boss.isDead)
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator FireCircularPattern()
    {
        yield return new WaitForSeconds(startTime);

        while (true)
        {
            FireBullets();

            yield return new WaitForSeconds(fiteRate);

            FireBullets();

            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void FireBullets()
    {
        float angleStep = 360f / bulletCount; // 탄알 간 각도
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // 각 탄알의 방향 계산
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

            // 탄알 생성 및 초기화
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            angle += angleStep; // 다음 탄알의 각도로 이동
        }
    }
}
