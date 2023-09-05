using System.Collections.Generic;
using UnityEngine;

public class CubesManager : MonoBehaviour
{
    [SerializeField]
    private CubeFactory cubeFactory;

    [Header("Spawn Options")]
    [SerializeField]
    private float offsetRadius;
    [SerializeField]
    private Vector2 spawnRegionSize;

    private List<Vector2> spawnPoints;
    private List<Cube> cubes;

    public void CreateCubes(int amount)
    {
        spawnPoints = PoissonDiscSampling.GeneratePoints(offsetRadius, spawnRegionSize);

        ClearCubes();
        cubes = new List<Cube>();

        for (int i = 0; i < amount; i++)
        {
            Cube cube = cubeFactory.GetProduct();
            cube.Initialize();
            PlaceCube(cube);
            cubes.Add(cube);
        }
    }

    private void PlaceCube(Cube cube)
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
            cubes[i].OnDespawned();
            cubes.RemoveAt(i);
        }
    }
}
