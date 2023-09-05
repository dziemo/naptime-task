using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CubesManager cubesManager;
    [SerializeField]
    private int cubesAmount;

    private void Start()
    {
        cubesManager.CreateCubes(cubesAmount);
    }
}
