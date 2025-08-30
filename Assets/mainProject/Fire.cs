using UnityEngine;
using DG.Tweening;
using System;
public class Fire : MonoBehaviour
{
    private Enemy target;
    private Vector3 targetDirection;
    public Weapon weapon;

    public float rotationSpeed;

    private void RotateTowardsTarget()
    {
        if (target == null || target.Health <= 0) return;
        targetDirection = (target.transform.position - weapon.transform.position).normalized;
        weapon.transform.DORotateQuaternion(Quaternion.LookRotation(targetDirection), rotationSpeed);
        if (IsTargetInSight(weapon.transform, target.transform))
        {
            FireWeapon(target.transform);
            // Target is in sight, fire weapon
        }
    }

    private void FireWeapon(Transform target)
    {
        weapon.Fire(target);
    }
    private void FindNearestEnemy()
    {
        // Implement logic to find the nearest enemy from EnemyManager
        Enemy nearestEnemy = null;
        float closestDistance = weapon.weaponProperties.range;

        foreach (var enemy in EnemyManager.Instance.GetAllActiveEnemies())
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy;
    }
    void Update()
    {
        FindNearestEnemy();
        RotateTowardsTarget();
        //RotateTowardsTarget();
    }
    public bool IsTargetInSight(Transform weapon, Transform target)
    {
        Vector3 directionToTarget = (target.position - weapon.position).normalized;
        float angle = Vector3.Angle(weapon.forward, directionToTarget);
        // Consider a small threshold for angle to account for precision issues
        return angle < 3f; // Adjust the threshold as needed
    }
}
