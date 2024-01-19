using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<MonoBehaviour> players;


    private void Awake()
    {
        StartCoroutine(DeactivatePlayers());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivatePlayers()
    {
        foreach (var player in players)
        {
            player.enabled = true;
        }
    }

    IEnumerator DeactivatePlayers()
    {
        yield return new WaitForEndOfFrame();

        players = FindObjectsOfType<PlayerInputHandler>().ToList<MonoBehaviour>();

        foreach (var player in players)
        {
            player.enabled = false;
        }
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MianMenu");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditosMenu");
    }

    public void ControlsScene()
    {
        SceneManager.LoadScene("Controles");
    }

    public void SelectCharacter()
    {
        // Change with the real scene name
        SceneManager.LoadScene("PlayerSetup");
    }

    public void HowToPlayScene()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
