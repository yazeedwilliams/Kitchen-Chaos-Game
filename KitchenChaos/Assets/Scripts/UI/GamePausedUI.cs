using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { 
            GameManger.Instance.TogglePauseGame(); 
        });
        mainMenuButton.onClick.AddListener(() => { 
            Loader.Load(Loader.Scene.MainMenuScene); 
        });
        optionsButton.onClick.AddListener(() => { 
            Hide();
            OptionsUI.Instance.Show(Show); 
        });
    }

    private void Start()
    {
        GameManger.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManger.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
