using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private StartButton startButton;
    [SerializeField]
    private GameObject menuParent;
    [SerializeField]
    private List<GameOptionToggle> gameOptionToggles = new List<GameOptionToggle>();

    private GameOption currentGameOption;

    private void OnEnable()
    {
        startButton.Button.onClick.AddListener(OnStartGame);

        foreach (GameOptionToggle toggle in gameOptionToggles)
        {
            toggle.OnOptionToggleChange += Toggle_OnOptionToggleChange;
        }
    }

    private void OnDisable()
    {
        startButton.Button.onClick.RemoveListener(OnStartGame);

        foreach (GameOptionToggle toggle in gameOptionToggles)
        {
            toggle.OnOptionToggleChange -= Toggle_OnOptionToggleChange;
        }
    }

    private void Toggle_OnOptionToggleChange(GameOption option, bool isOn)
    {
        if (isOn)
        {
            currentGameOption = option;
            startButton.ActivateButton();
        }
        else
        {
            currentGameOption = null;
            startButton.DeactivateButton();
        }
    }

    private void OnStartGame()
    {
        gameManager.StartGame(currentGameOption);
        Hide();
    }

    public void Show()
    {
        menuParent.SetActive(true);
    }

    public void Hide()
    {
        menuParent.SetActive(false);
    }
}
