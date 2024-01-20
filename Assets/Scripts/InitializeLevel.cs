using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField] private AudioClip gameClip;
    [SerializeField] private Transform[] playerSpawns;
    [SerializeField] private GameObject[] playerPrefab;

    [SerializeField] private HUDManager hudManager;

    // Start is called before the first frame update
    void Awake()
    {
        AudioManager.Instance.StopBackgroundClip();

        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab[playerConfigs[i].Character], playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);

            switch (i)
            {
                case 0: // Player1
                    hudManager.SetP1Image(playerConfigs[i].Character);
                    break;
                case 1: // Player2
                    hudManager.SetP2Image(playerConfigs[i].Character);
                    break;
            }
        }
    }

    private void Start()
    {
        AudioManager.Instance.PlaySound(gameClip);

    }


}
