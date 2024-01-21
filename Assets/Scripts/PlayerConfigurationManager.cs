using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> _playerConfigs = new List<PlayerConfiguration>();

    [SerializeField] private int maxPlayers = 2;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Trying to create another instance");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            //_playerConfigs = new List<PlayerConfiguration>();
        }

        var pcs = GameObject.FindObjectsOfType<SpawnPlayerSetupMenu>().ToList();
        if (pcs.Count > 0)
        {
            _playerConfigs.Clear();
            for (int i = 0; i < pcs.Count; i++)
            {
                Destroy(pcs[i].gameObject);

            }
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        _playerConfigs[index].PlayerMaterial = color;
    }

    public void SetPlayerCharacter(int index, int character)
    {
        _playerConfigs[index].Character = character;
    }

    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.Count == maxPlayers && _playerConfigs.All(p => p.IsReady == true))
        {
            //SceneManager.LoadScene("Multiplayer"); // ES-2024.01.07: comentamos este para pruebas
            // SceneManager.LoadScene("ES_Test"); // ES-2024.01.07: agregamos esta escena para Test
            SceneManager.LoadScene("Game"); //Test de juego con UI
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);

        if (!_playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return _playerConfigs;
    }
}

