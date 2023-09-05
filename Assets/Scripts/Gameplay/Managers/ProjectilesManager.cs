using System.Collections.Generic;
using UnityEngine;

public class ProjectilesManager : MonoBehaviour
{
    [SerializeField]
    private ProjectilesFactory projectilesFactory;

    private List<Projectile> activeProjectiles = new List<Projectile>();
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public Projectile GetProjectile()
    {
        Projectile newProjectile = projectilesFactory.GetProduct();
        activeProjectiles.Add(newProjectile);
        newProjectile.OnDespawned += Projectile_OnDespawn;

        return newProjectile;
    }

    private void Projectile_OnDespawn(Projectile projectile)
    {
        activeProjectiles.Remove(projectile);
        projectile.OnDespawned -= Projectile_OnDespawn;
    }

    private void Update()
    {
        MoveProjectiles();
        CheckIfOutOfBounds();
        CheckProjectileCollisions();
    }

    private void CheckProjectileCollisions()
    {
        for (int i = activeProjectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = activeProjectiles[i];
            projectile.CheckForCollision();
        }
    }

    private void MoveProjectiles()
    {
        foreach (Projectile projectile in activeProjectiles)
        {
            projectile.MoveProjectile();
        }
    }

    private void CheckIfOutOfBounds()
    {
        for (int i = 0; i < activeProjectiles.Count; i++)
        {
            Projectile projectile = activeProjectiles[i];
            Vector3 viewportPosition = cam.WorldToViewportPoint(projectile.transform.position);
            if (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1)
            {
                return;
            }

            activeProjectiles.Remove(projectile);
            projectile.OnDespawn();
        }
    }
}
