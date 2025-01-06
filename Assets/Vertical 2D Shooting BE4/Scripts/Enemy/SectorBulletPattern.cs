using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorBulletPattern : MonoBehaviour
{
    public GameObject bulletPrefab; // ź�� ������
    private Boss boss;

    private float startTime = 3;
    public int bulletCount = 10;    // ź�� ����
    public float bulletSpeed = 3f;  // ź�� �ӵ�
    public float fireInterval = 5f; // �߻� ����
    public float spreadAngle = 60f; // ��ä�� ���� (�� ����)

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
        float startAngle = -spreadAngle - 60f; // ��ä�� ���� ����
        float angleStep = spreadAngle / (bulletCount - 1); // ź�� �� ����
        float angle = startAngle;

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
