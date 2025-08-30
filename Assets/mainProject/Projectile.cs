using UnityEngine;
// Add this if needed for ProjectileManager
// using YourNamespace; // Uncomment and set if ProjectileManager is in a namespace

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabRef; // store prefab reference (set when spawned)
    private float timer;
    private Vector3 velocity;
    private WeaponProperties weaponProperties;
    private Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, WeaponProperties weaponProperties)
    {
        velocity = direction.normalized ;
        timer = 0f;
        ProjectileManager.Instance.Register(this);
        Debug.LogError("Projectile launched");
        this.weaponProperties = weaponProperties;
    }

    public void MoveAndTick(float deltaTime)
    {
        rigidbody.position += (velocity ) * (weaponProperties.projectileSpeed * Time.deltaTime); 
        timer += deltaTime;
        if (timer >= 5f)
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
            BlastFx blastfx = BlastFXPoolManager.Instance.GetBlastFX(weaponProperties.projectileBlastFX);
            blastfx.transform.position = transform.position;
            blastfx.gameObject.SetActive(true);
            Despawn();
            other.gameObject.GetComponent<Enemy>().TakeDamage(weaponProperties.damage);
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