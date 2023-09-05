using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CubesManager cubesManager;
    [SerializeField]
    private int cubesAmount;

    public void StartGame(GameOption currentGameOption)
    {
        cubesManager.CreateCubes(currentGameOption.ObjectsCount);
    }
}
