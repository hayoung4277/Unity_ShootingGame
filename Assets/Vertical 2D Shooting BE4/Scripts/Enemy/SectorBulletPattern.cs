using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorBulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab; // 탄알 프리팹
    private Boss boss;

    private float startTime = 3;
    public int bulletCount = 10;    // 탄알 개수
    public float bulletSpeed = 3f;  // 탄알 속도
    public float fireInterval = 5f; // 발사 간격
    public float spreadAngle = 60f; // 부채꼴 각도 (도 단위)

    private void Awake()
    {
        var findGo = GameObject.FindWithTag("Boss");
        boss = findGo.GetComponent<Boss>();
    }

    private void Start()
    {
        StartCoroutine(FireFanPattern());
    }

    private void Update()
    {
        if(boss.isDead)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator FireFanPattern()
    {
        yield return new WaitForSeconds(startTime);

        while (true)
        {
            FireBullets();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void FireBullets()
    {
        float startAngle = -spreadAngle - 60f; // 부채꼴 시작 각도
        float angleStep = spreadAngle / (bulletCount - 1); // 탄알 간 각도
        float angle = startAngle;

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
