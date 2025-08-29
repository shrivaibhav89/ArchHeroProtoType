using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 3f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    public void SpawnEnemy()
    {
        Enemy enemy = EnemyManager.Instance.GetEnemy(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.transform.position = spawnPoint.position + Random.insideUnitSphere * 5f;
        enemy.transform.position = new Vector3(enemy.transform.position.x, 1, enemy.transform.position.z);
    }
}
