using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    private Dictionary<GameObject, Queue<Projectile>> poolDictionary = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public Projectile GetProjectile(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(prefab))
            poolDictionary[prefab] = new Queue<Projectile>();

        Projectile projectile;

        if (poolDictionary[prefab].Count > 0)
        {
            projectile = poolDictionary[prefab].Dequeue();

        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                Projectile newProjectile = Instantiate(prefab).GetComponent<Projectile>();
                newProjectile.prefabRef = prefab;
                newProjectile.gameObject.SetActive(false);
                poolDictionary[prefab].Enqueue(newProjectile);
            }
            projectile = poolDictionary[prefab].Dequeue();

        }

        projectile.prefabRef = prefab;
        projectile.transform.SetPositionAndRotation(position, rotation);
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    public void ReturnProjectile(GameObject prefab, Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        if (projectile.prefabRef == null)
        {
            projectile.prefabRef = prefab;
        }
        if (!poolDictionary.ContainsKey(projectile.prefabRef))
            poolDictionary[projectile.prefabRef] = new Queue<Projectile>();
        poolDictionary[projectile.prefabRef].Enqueue(projectile);
    }
}