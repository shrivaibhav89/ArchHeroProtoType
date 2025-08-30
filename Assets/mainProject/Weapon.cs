using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponProperties weaponProperties;
    private int currentBullets;
    public bool isReloading = false;
    private float nextTimeToFire = 0f;
    [SerializeField] private Transform firepoint;
    Vector3 targetDirection;


    public void Fire(Transform target)
    {
        if (isReloading) return;

        if (Time.time < nextTimeToFire) return;
       
        nextTimeToFire = Time.time + 1f / weaponProperties.fireRate;
        Projectile projectile = ProjectilePool.Instance.GetProjectile(weaponProperties.projectilePrefab, firepoint.position, firepoint.rotation);
        targetDirection = (target.position - firepoint.position).normalized;

        projectile.Launch(targetDirection, weaponProperties);
        Debug.Log("Weapon Fired! Damage: " + weaponProperties.damage);
        currentBullets--;
        if (currentBullets <= 0)
        {
            currentBullets = 0;
            StartCoroutine(Reload());
        }
        EventManager.TriggerEvent(UIEvent.OnAmmoChanged, currentBullets);
    }

    private IEnumerator Reload()
    {
        EventManager.TriggerEvent(GameEvent.OnWeaponReload, weaponProperties.reloadTime);
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(weaponProperties.reloadTime);
        currentBullets = weaponProperties.magazineSize;
        isReloading = false;
        EventManager.TriggerEvent(UIEvent.OnAmmoChanged, currentBullets);
    }

}
