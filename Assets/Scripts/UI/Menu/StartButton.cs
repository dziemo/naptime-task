using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private GameObject dpadImage;

    [SerializeField]
    private Button button;
    public Button Button => button;

    public void ActivateButton()
    {
        button.interactable = true;
        buttonImage.sprite = activeSprite;
        dpadImage.SetActive(true);
    }

    public void DeactivateButton()
    {
        button.interactable = false;
        buttonImage.sprite = inactiveSprite;
        dpadImage.SetActive(false);
    }
}
