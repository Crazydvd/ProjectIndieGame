using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [SerializeField] private float _givenTime = 180;

    [Header("What the rotation of the light will be at when the timer runs out")]
    public Vector3 _targetRotation = new Vector3(-3f, -30f, 0);

    private Text _timerUI;
    private PlayersHandler _playerHandler;
    private GameObject _light;

    private Vector3 _originalRotation;
    private Vector3 _delta;

    private float _scalar = 0;
    private float _time;
    private float _secondsTime;
    private bool _endGame;

    void Start()
    {
        _timerUI = GetComponent<Text>();
        _playerHandler = Camera.main.GetComponent<PlayersHandler>();
        _timerUI.text = ((int)_givenTime).ToString();
        _light = GameObject.FindWithTag("Light");
        if (_light == null)
        {
            throw new System.Exception("YOU DIDN'T APPLY A TAG TO THE LIGHT");
        }

        _originalRotation = _light.transform.rotation.eulerAngles;
        _time = _givenTime;
        _delta = _targetRotation - _originalRotation;
    }

    void Update()
    {
        rotateLight();
        int winnerID = -1;
        if (_givenTime <= 0)
        {
            string winner = "No one";
            PlayerStatus bestPlayerStats = null;

            foreach (GameObject player in _playerHandler.GetPlayers())
            {
                PlayerStatus playerStats = player.GetComponentInChildren<PlayerStatus>();
                PlayerParameters playerParameters = player.GetComponent<PlayerParameters>();

                if (bestPlayerStats == null)
                {
                    bestPlayerStats = playerStats;
                    winner = player.name;
                    winnerID = playerParameters.PLAYER;
                    continue;
                }

                if (playerStats.GetLives() > bestPlayerStats.GetLives())
                {
                    bestPlayerStats = playerStats;
                    winner = player.name;
                    winnerID = playerParameters.PLAYER;
                }
                else if (playerStats.GetLives() == bestPlayerStats.GetLives())
                {
                    if (playerStats.GetDamage() < bestPlayerStats.GetDamage())
                    {
                        bestPlayerStats = playerStats;
                        winner = player.name;
                        winnerID = playerParameters.PLAYER;
                    }
                    else if (playerStats.GetDamage() == bestPlayerStats.GetDamage())
                    {
                        winner = "No one";
                        winnerID = playerParameters.PLAYER;
                    }
                }
            }

            _playerHandler.EndGame(winner, winnerID);
            Destroy(this);
        }

        if (_givenTime <= 10)
        {
            _secondsTime -= Time.deltaTime;
            if (_secondsTime <= 0)
            {
                _secondsTime = 1;
                _timerUI.color = new Color(1, 0, 0);
                _timerUI.fontSize = _timerUI.fontSize + 1;
                FMODUnity.RuntimeManager.PlayOneShot("event:/bing");
            }
        }

        _givenTime -= Time.deltaTime;
        _timerUI.text = ((int)_givenTime).ToString() + "s";
    }

    private void rotateLight()
    {
        _scalar += (1 / _time) * Time.deltaTime;
        _light.transform.rotation = Quaternion.Euler(_originalRotation + _delta * _scalar);
    }

    public void StopTimer()
    {
        Destroy(this);
    }
}
