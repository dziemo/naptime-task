using UnityEngine;

public class CubeFactory : PooledFactory<Cube>
{
    [SerializeField]
    private Transform cubesParent;

    protected override Cube OnCreate()
    {
        Cube newCube = base.OnCreate();
        newCube.transform.SetParent(cubesParent);
        return newCube;
    }
}
