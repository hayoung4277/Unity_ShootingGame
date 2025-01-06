using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyAPrefab;
    public GameObject enemyBPrefab;
    public GameObject enemyCPrefab;

    public float spawnAInterval = 3f;
    public float spawnBInterval = 4f;
    public float spawnCInterval = 10f;

    public float spawnXPos;

    public float damage = 20f;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesA());
        StartCoroutine(SpawnEnemiesB());
        StartCoroutine(SpawnEnemiesC());
    }

    public void Stop()
    {
        enabled = false;

        StopAllCoroutines();
    }

    IEnumerator SpawnEnemiesA()
    {
        yield return new WaitForSeconds(spawnAInterval);

        while (true)
        {
            SpawnEnemyA();
            yield return new WaitForSeconds(spawnAInterval);
        }
    }
    IEnumerator SpawnEnemiesB()
    {
        while (true)
        {
            SpawnEnemyB();
            yield return new WaitForSeconds(spawnBInterval);
        }
    }

    IEnumerator SpawnEnemiesC()
    {
        yield return new WaitForSeconds(spawnCInterval);

        while (true)
        {
            SpawnEnemyC();
            yield return new WaitForSeconds(spawnCInterval);
        }
    }

    private void SpawnEnemyB()
    {
        float spacing = 2f;
        Vector3 startPosition = new Vector3(-spacing, 6, 0);

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = startPosition + new Vector3(i * spacing, 0, 0);
            GameObject enemy = Instantiate(enemyBPrefab, position, Quaternion.identity);
            enemy.GetComponent<EnemyB>().Move();
        }
    }

    private void SpawnEnemyA()
    {
        float randomValue = Random.value;

        if (randomValue < 0.5f)
        {
            spawnXPos = -3f;

        }
        else
        {
            spawnXPos = 3f;
        }

        float spacing = 1.5f; // 적 간 간격
        Vector3 startPosition = new Vector3(spawnXPos, 5, 0); // 시작 위치 (왼쪽 위)

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = startPosition + new Vector3(i * spacing, -i * spacing, 0);
            GameObject enemy = Instantiate(enemyAPrefab, position, Quaternion.identity);
            enemy.GetComponent<EnemyA>().Move(); // 이동 호출
        }
    }

    private void SpawnEnemyC()
    {
        float spacing = 2f;
        Vector3 startPosition = new Vector3(-spacing, 5, 0);

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = startPosition + new Vector3(i * spacing, 0, 0);
            GameObject enemy = Instantiate(enemyCPrefab, position, Quaternion.identity);
            enemy.GetComponent<EnemyC>().Move();
        }
    }
}
