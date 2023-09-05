using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenShots;
    [SerializeField]
    private Collider2D collider;

    private ProjectilesManager projectilesManager;
    private Coroutine shootCoroutine;

    public void Initialize(ProjectilesManager projectilesManager)
    {
        this.projectilesManager = projectilesManager;
    }

    public void StartShooting()
    {
        shootCoroutine = StartCoroutine(ShootSequence());
    }

    public void StopShooting()
    {
        StopCoroutine(shootCoroutine);
    }

    private IEnumerator ShootSequence()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        Shoot();
        shootCoroutine = StartCoroutine(ShootSequence());
    }

    private void Shoot()
    {
        Projectile projectile = projectilesManager.GetProjectile();
        projectile.Launch(transform.position, transform.up, collider);
    }
}
