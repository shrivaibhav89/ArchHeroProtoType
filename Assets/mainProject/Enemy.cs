using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabRef;
    [SerializeField] private EnemyProperties enemyProperties;
    [SerializeField] private float health;

    public float Health { get { return health; } }

    private NavMeshAgent nvm;
    private enum EnemyState {Chasing, Attacking }
    private EnemyState currentState;

    void Awake()
    {
        nvm = GetComponent<NavMeshAgent>();
        currentState = EnemyState.Chasing;
    }

    // Example: Call this when enemy should be despawned
    void OnEnable()
    {
        health = enemyProperties.maxHealth;
        nvm.speed = enemyProperties.speed;
        nvm.stoppingDistance = enemyProperties.attackRange;
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

    public void MoveTowardTarget(Transform target, float deltaTime)
    {
        if (target == null ) return;

        if(currentState == EnemyState.Chasing && Vector3.Distance(transform.position, target.position) <= enemyProperties.attackRange)
        {
            currentState = EnemyState.Attacking;
            // Switch to attacking behavior if needed
        }
        else if(currentState == EnemyState.Attacking && Vector3.Distance(transform.position, target.position) > enemyProperties.attackRange)
        {
            currentState = EnemyState.Chasing;
            // Switch back to chasing behavior if needed
        }
        // Implement movement logic here
        if(currentState == EnemyState.Attacking)
        {
            // Implement attack logic here (e.g., reduce player health)
            return; // Skip movement when attacking
        }
        nvm.SetDestination(target.position);
    }
}