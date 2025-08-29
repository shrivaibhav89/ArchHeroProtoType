using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance;
    private readonly List<Projectile> activeProjectiles = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        for (int i = activeProjectiles.Count - 1; i >= 0; i--)
        {
            activeProjectiles[i].MoveAndTick(dt);
        }
    }

    public void Register(Projectile proj)
    {
        if (!activeProjectiles.Contains(proj))
            activeProjectiles.Add(proj);
    }

    public void Unregister(Projectile proj)
    {
        activeProjectiles.Remove(proj);
    }
}
