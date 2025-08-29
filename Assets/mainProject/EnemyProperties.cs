using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemyProperties : ScriptableObject
{
    public float maxHealth = 20f;
    public float damage = 5f;
    public float speed = 2f;
}
