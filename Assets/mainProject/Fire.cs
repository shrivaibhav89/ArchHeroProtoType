using UnityEngine;
using DG.Tweening;
using System;
public class Fire : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 targetDirection;
    public Weapon weapon;

    public float enemyCheckRange = 10f;
    public float rotationSpeed;

    private void RotateTowardsTarget()
    {
        if (target == null) return;
        targetDirection = (target.position - transform.position).normalized;
        // dont want vertical rotation
        targetDirection.y = 0;
        // rotate weapon towards target
        // weapon.DORotateQuaternion(Quaternion.LookRotation(targetDirection), rotationSpeed);
        // fire when rotation done
        weapon.transform.DORotateQuaternion(Quaternion.LookRotation(targetDirection), rotationSpeed);
        if (IsTargetInSight(weapon.transform, target))
        {
            FireWeapon();
            // Target is in sight, fire weapon
        }
    }

    private void FireWeapon()
    {
        weapon.Fire();
    }
    private void FindNearestEnemy()
    {
        // Implement logic to find the nearest enemy from EnemyManager
        Transform nearestEnemy = null;
        float closestDistance = enemyCheckRange;

        foreach (var enemy in EnemyManager.Instance.GetAllActiveEnemies())
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        target = nearestEnemy;
    }
    void Update()
    {
        FindNearestEnemy();
        RotateTowardsTarget();
    }
    public bool IsTargetInSight(Transform weapon, Transform target)
    {
        Vector3 directionToTarget = (target.position - weapon.position).normalized;
        float angle = Vector3.Angle(weapon.forward, directionToTarget);
        // Check if target is in front (dot > 0) and within ±15 degrees
        return angle <= 15f && Vector3.Dot(weapon.forward, directionToTarget) > 0f;
    }
}
