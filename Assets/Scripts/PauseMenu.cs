using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, Players.IPauseMenuActions
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Selectable firstElementToSelect;
    private Players controls;

    private void Start()
    {
        controls = new Players();
        controls.PauseMenu.Enable();
        controls.PauseMenu.SetCallbacks(this);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MianMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!pausePanel.activeSelf)
            {
                firstElementToSelect?.Select();
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
            }

        }
    }
}
