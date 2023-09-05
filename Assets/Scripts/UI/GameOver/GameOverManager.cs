using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverParent;
    [SerializeField]
    private MainMenuManager mainMenuManager;
    [SerializeField]
    private Button mainMenuButton;

    private void OnEnable()
    {
        mainMenuButton.onClick.AddListener(OnMainMenuRedirect);
    }

    private void OnDisable()
    {
        mainMenuButton.onClick.RemoveListener(OnMainMenuRedirect);
    }

    private void OnMainMenuRedirect()
    {
        Hide();
        mainMenuManager.Show();
    }

    public void Show()
    {
        gameOverParent.SetActive(true);
    }

    public void Hide()
    {
        gameOverParent.SetActive(false);
    }
}
