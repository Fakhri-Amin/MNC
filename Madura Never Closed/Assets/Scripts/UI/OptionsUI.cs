using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        sfxSlider.onValueChanged.AddListener(delegate
        {
            SoundManager.Instance.SetVolume(sfxSlider.value);
        });

        musicSlider.onValueChanged.AddListener(delegate
        {
            MusicManager.Instance.SetVolume(musicSlider.value);
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        sfxSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
        sfxSlider.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
