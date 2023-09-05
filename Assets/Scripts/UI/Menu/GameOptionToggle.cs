using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionToggle : MonoBehaviour
{
    public event Action<GameOption, bool> OnOptionToggleChange;

    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Image buttonImage;

    [SerializeField]
    private Sprite unselectedSprite;
    [SerializeField]
    private Sprite selectedSprite;

    [SerializeField]
    private float selectedScale;

    [SerializeField]
    private GameOption gameOption;


    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnToggleChange);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleChange);
    }

    private void OnToggleChange(bool isOn)
    {
        buttonImage.sprite = isOn ? selectedSprite : unselectedSprite;
        transform.localScale = isOn ? Vector3.one * selectedScale : Vector3.one;

        OnOptionToggleChange?.Invoke(gameOption, isOn);
    }
}
