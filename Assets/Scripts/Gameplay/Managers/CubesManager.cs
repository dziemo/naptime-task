using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    [SerializeField]
    private CubeFactory cubeFactory;
    [SerializeField]
    private ProjectilesManager projectilesManager;

    [Header("Spawn Options")]
    [SerializeField]
    private float offsetRadius;
    [SerializeField]
    private Vector2 spawnRegionSize;
    [SerializeField]
    private float hitHideTime;

    private List<Vector2> spawnPoints;
    private List<CubeController> cubes;

    public void CreateCubes(int amount)
    {
        spawnPoints = PoissonDiscSampling.GeneratePoints(offsetRadius, spawnRegionSize);

        ClearCubes();
        cubes = new List<CubeController>();

        for (int i = 0; i < amount; i++)
        {
            CubeController cube = cubeFactory.GetProduct();
            cube.SetupDependencies(projectilesManager);
            cube.Initialize();
            PlaceCube(cube);
            cubes.Add(cube);

            cube.OnHit += Cube_OnHit;
        }
    }

    private void Cube_OnHit(CubeController cube)
    {
        spawnPoints.Add(cube.transform.position + (Vector3)(spawnRegionSize / 2));

        if (cube.CurrentHealth > 0)
        {
            StartCoroutine(ReappearCube(cube));
        }
        else
        {
            cube.OnHit -= Cube_OnHit;
        }
    }

    private IEnumerator ReappearCube(CubeController cube)
    {
        yield return new WaitForSeconds(hitHideTime);
        PlaceCube(cube);
        cube.Reappear();
    }

    private void PlaceCube(CubeController cube)
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnPoints.Remove(spawnPoint);
        cube.transform.position = spawnPoint - (spawnRegionSize / 2);
    }

    private void ClearCubes()
    {
        if (cubes == null)
        {
            return;
        }

        for (int i = cubes.Count - 1; i >= 0; i--)
        {
            cubes[i].OnDespawn();
            cubes.RemoveAt(i);
        }
    }
}
