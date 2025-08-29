using UnityEngine;
// Add this if needed for ProjectileManager
// using YourNamespace; // Uncomment and set if ProjectileManager is in a namespace

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabRef; // store prefab reference (set when spawned)

    private float lifeTime = 5f;
    private float timer;
    [SerializeField] private float speed = 20f;
    private Vector3 velocity;
    private float damage;

    public void Launch(Vector3 direction, WeaponProperties weaponProperties)
    {
        velocity = direction.normalized * speed;
        timer = 0f;
        ProjectileManager.Instance.Register(this);
        Debug.LogError("Projectile launched");
        damage = weaponProperties.damage;
    }

    public void MoveAndTick(float deltaTime)
    {
        transform.position += velocity * deltaTime;
        timer += deltaTime;
        if (timer >= lifeTime)
            Despawn();
    }

    void OnEnable()
    {
        // Do not register here; call Launch when firing
    }

    void OnDisable()
    {
        ProjectileManager.Instance.Unregister(this);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile hit: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            Despawn();
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void Despawn()
    {
        ProjectileManager.Instance.Unregister(this);
        if (prefabRef != null)
            ProjectilePool.Instance.ReturnProjectile(prefabRef, this);
        else
            gameObject.SetActive(false);
    }
}