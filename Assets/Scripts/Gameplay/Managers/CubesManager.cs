using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    public event Action OnLastCubeLeft;

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
    private List<CubeController> activeCubes;
    private Vector2 halfSpawnRegionSize;

    private void Awake()
    {
        halfSpawnRegionSize = spawnRegionSize / 2;
    }

    public void CreateCubes(int amount)
    {
        spawnPoints = PoissonDiscSampling.GeneratePoints(offsetRadius, spawnRegionSize);

        ClearCubes();
        activeCubes = new List<CubeController>();

        for (int i = 0; i < amount; i++)
        {
            CubeController cube = cubeFactory.GetProduct();
            cube.SetupDependencies(projectilesManager);
            cube.Initialize();
            PlaceCube(cube);
            activeCubes.Add(cube);

            cube.OnDamageTaken += Cube_OnHit;
        }
    }

    private void Cube_OnHit(CubeController cube)
    {
        spawnPoints.Add(cube.transform.position + (Vector3)halfSpawnRegionSize);

        if (cube.CurrentHealth > 0)
        {
            StartCoroutine(ReappearCube(cube));
        }
        else
        {
            cube.OnDamageTaken -= Cube_OnHit;
            activeCubes.Remove(cube);
            CheckForLastCube();
        }
    }

    private void CheckForLastCube()
    {
        if (activeCubes.Count == 1)
        {
            OnLastCubeLeft?.Invoke();
            activeCubes[0].StopBehaviours();
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
        Vector2 spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)];
        spawnPoints.Remove(spawnPoint);
        cube.transform.position = spawnPoint - halfSpawnRegionSize;
    }

    private void ClearCubes()
    {
        if (activeCubes == null)
        {
            return;
        }

        for (int i = activeCubes.Count - 1; i >= 0; i--)
        {
            activeCubes[i].OnDespawn();
            activeCubes.RemoveAt(i);
        }
    }
}
