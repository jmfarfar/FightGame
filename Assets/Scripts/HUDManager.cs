using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private float maxLifeP1 = 100;

    [Header("Player1")]
    [SerializeField] private Image lifeBarP1;
    float currentLifeP1;
    public bool IsLifeP1Empty { get; set; }

    [Header("Player2")]
    [SerializeField] private Image lifeBarP2;
    float currentLifeP2;
    public bool IsLifeP2Empty { get; set; }

    [Header("Time")]
    [SerializeField] private TextMeshProUGUI timer;
    float maxTime = 99f;
    float time;
    int currentTime;

    //[SerializeField] private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeP1 = currentLifeP2 = maxLifeP1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifeP1 <= 0)
        {
            IsLifeP1Empty = true;
        }

        if (currentLifeP2 <= 0)
        {
            IsLifeP2Empty = true;
        }

        if (time < maxTime)
        {
            Timer();

        }
        else
        {
            // time out
            // declarar como ganador al jugador con mas vida
            // desplegar las pantallas de opciones a los jugadores
            if (currentLifeP1 > currentLifeP2)
                IsLifeP2Empty = true;
            else
                IsLifeP1Empty = true;
            Debug.Log("Time out");
            //gameOverPanel.SetActive(true);
        }
    }

    private void Timer()
    {
        time += Time.deltaTime;
        currentTime = (int)time;
        timer.text = ((int)maxTime - currentTime).ToString();

    }

    public void P1DamageReceived(int damage)
    {
        currentLifeP1 -= damage;
        lifeBarP1.fillAmount = currentLifeP1 / maxLifeP1;
    }

    public void P2DamageReceived(int damage)
    {
        currentLifeP2 -= damage;
        lifeBarP2.fillAmount = currentLifeP2 / maxLifeP1;
    }
}
