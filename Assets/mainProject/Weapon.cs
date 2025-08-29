using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponProperties weaponProperties;
    public int currentBullets;
    public bool isReloading = false;
    private float nextTimeToFire = 0f;
   [SerializeField] private Transform firepoint;

    

    public void Fire()
    {
        if (isReloading) return;

        if (Time.time < nextTimeToFire) return;
        nextTimeToFire = Time.time + 1f / weaponProperties.fireRate;
        Projectile projectile = ProjectilePool.Instance.GetProjectile(weaponProperties.projectilePrefab, firepoint.position, firepoint.rotation);

        projectile.Launch(firepoint.forward, weaponProperties);
        if (currentBullets > 0)
        {
            Debug.Log("Weapon Fired! Damage: " + weaponProperties.damage);
            currentBullets--;
        }
        else
        {
            Debug.Log("Out of bullets!");
        }
    }

}
