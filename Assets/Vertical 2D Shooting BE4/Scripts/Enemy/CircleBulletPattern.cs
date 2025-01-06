using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Boss boss;

    private float startTime = 5;
    public int bulletCount = 12;    // ź�� ����
    public float bulletSpeed = 3f;  // ź�� �ӵ�
    public float fiteRate = 1f;
    public float fireInterval = 7f; // �߻� ����

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
        float angleStep = 360f / bulletCount; // ź�� �� ����
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // �� ź���� ���� ���
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 bulletDirection = new Vector3(dirX, dirY, 0f);

            // ź�� ���� �� �ʱ�ȭ
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            angle += angleStep; // ���� ź���� ������ �̵�
        }
    }
}
