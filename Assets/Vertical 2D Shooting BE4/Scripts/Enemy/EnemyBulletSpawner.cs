using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform parent;

    public float damage = 20f;
    public float fireRate = 1.5f;
    private float currentFireTime;

    private void Awake()
    {
        currentFireTime = 0;
    }

    private void Update()
    {
        currentFireTime += Time.deltaTime;

        if (currentFireTime >= fireRate)
        {
            currentFireTime = 0;

            GameObject bullet = Instantiate(bulletPrefab, parent);

        }
    }
}
