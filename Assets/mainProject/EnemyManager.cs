using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    // Dictionary to hold pools for each enemy prefab
    private Dictionary<GameObject, Queue<Enemy>> poolDictionary = new();
    private List<Enemy> activeEnemies = new();
    private Transform playerTransform;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Get an enemy from the pool or instantiate if needed
    public Enemy GetEnemy(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
            poolDictionary[prefab] = new Queue<Enemy>();

        Enemy enemy;

        if (poolDictionary[prefab].Count > 0)
        {
            enemy = poolDictionary[prefab].Dequeue();
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                Enemy newEnemy = Instantiate(prefab).GetComponent<Enemy>();
                newEnemy.prefabRef = prefab;
                newEnemy.gameObject.SetActive(false);
                poolDictionary[prefab].Enqueue(newEnemy);
            }
            enemy = poolDictionary[prefab].Dequeue();
        }
        enemy.prefabRef = prefab;
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.gameObject.SetActive(true);
        activeEnemies.Add(enemy);
        return enemy;
    }

    public List<Enemy> GetAllActiveEnemies()
    {
        return activeEnemies;
    }

    // Return an enemy to the pool
    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        if (enemy.prefabRef == null)
        {
            Debug.LogWarning("Enemy prefab reference is null. Cannot return to pool.");
            Destroy(enemy.gameObject);
            return;
        }
        if (!poolDictionary.ContainsKey(enemy.prefabRef))
            poolDictionary[enemy.prefabRef] = new Queue<Enemy>();
        poolDictionary[enemy.prefabRef].Enqueue(enemy);
        activeEnemies.Remove(enemy);
    }

    void FixedUpdate()
    {
        foreach (var enemy in activeEnemies)
        {
            if (enemy.Health >= 0 && enemy.gameObject.activeInHierarchy)
            {
                enemy.MoveTowardTarget(playerTransform, Time.fixedDeltaTime);
            }

            // Implement enemy behavior updates here
        }
    }
}

// Basic Enemy class for pooling (extend as needed)

