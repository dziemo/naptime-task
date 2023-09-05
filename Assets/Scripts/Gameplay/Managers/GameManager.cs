using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CubesManager cubesManager;
    [SerializeField]
    private int cubesAmount;
    [SerializeField]
    private GameOverManager gameOverManager;

    private void OnEnable()
    {
        cubesManager.OnLastCubeLeft += CubesManager_OnLastCubeLeft;
    }

    private void OnDisable()
    {
        cubesManager.OnLastCubeLeft -= CubesManager_OnLastCubeLeft;
    }

    private void CubesManager_OnLastCubeLeft()
    {
        EndGame();
    }

    private void EndGame()
    {
        gameOverManager.Show();
    }

    public void StartGame(GameOption currentGameOption)
    {
        cubesManager.CreateCubes(currentGameOption.ObjectsCount);
    }
}
