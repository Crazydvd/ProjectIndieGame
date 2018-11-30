using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private float _givenTime = 180;

    private Text _timerUI;
    private PlayersHandler _playerHandler;

    void Start()
    {
        _timerUI = GetComponent<Text>();
        _playerHandler = Camera.main.GetComponent<PlayersHandler>();
        _timerUI.text = ((int)_givenTime).ToString();
    }
    
    void Update()
    {
        if (_givenTime <= 0)
        {
            string winner = "No one";
            PlayerStatus bestPlayerStats = null;

            foreach (GameObject player in _playerHandler.GetPlayers())
            {
                PlayerStatus playerStats = player.GetComponentInChildren<PlayerStatus>();

                if (bestPlayerStats == null)
                {
                    bestPlayerStats = playerStats;
                    winner = player.name;
                    continue;
                }

                if (playerStats.GetLives() > bestPlayerStats.GetLives())
                {
                    bestPlayerStats = playerStats;
                    winner = player.name;
                }
                else if(playerStats.GetLives() == bestPlayerStats.GetLives())
                {
                    if (playerStats.GetDamage() < bestPlayerStats.GetDamage())
                    {
                        bestPlayerStats = playerStats;
                        winner = player.name;
                    }
                    else if (playerStats.GetDamage() == bestPlayerStats.GetDamage())
                    {
                        winner = "No one";
                    }
                }
            }

            _playerHandler.EndGame(winner);
            Destroy(this, 0);
        }

        _givenTime -= Time.deltaTime;
        _timerUI.text = ((int)_givenTime).ToString(); 
    }
}
