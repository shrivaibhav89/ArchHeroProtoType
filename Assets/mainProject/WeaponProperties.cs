using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
public class WeaponProperties : ScriptableObject
{
    public float damage;
    public float range;
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public float projectileSpeed;
    public AudioClip fireSound;
    public GameObject projectilePrefab;
    public GameObject muzzleFlash;
    public GameObject projectileBlastFX;
}

