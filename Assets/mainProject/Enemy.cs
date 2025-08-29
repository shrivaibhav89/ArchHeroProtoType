using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabRef;
    [SerializeField] private EnemyProperties enemyProperties;
    private float health;

    // Example: Call this when enemy should be despawned
    void OnEnable()
    {
        health = enemyProperties.maxHealth;
    }
    public void Despawn()
    {
        Debug.Log("Enemy despawned");
      //  health = enemyProperties.maxHealth;
        EnemyManager.Instance.ReturnEnemy(this);


    }

    public void TakeDamage(float damage)
    {
        // Implement damage handling logic
        health -= damage;
        Debug.Log("Enemy took damage: " + damage);
        if (health <= 0)
        {
            Debug.Log("Enemy died");
            Despawn();
        }
    }
}