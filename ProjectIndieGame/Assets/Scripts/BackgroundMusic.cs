using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance _bgMusic;
    FMOD.Studio.ParameterInstance _lifes;

    private PlayersHandler _playerHandler;

    void Start()
    {
        _playerHandler = Camera.main.GetComponent<PlayersHandler>();

        _bgMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack");
        _bgMusic.getParameter("Lifes", out _lifes);
        _bgMusic.start();
        _bgMusic.release();

    }

    public void SetLifesParameter(int pParameter)
    {
        foreach (GameObject player in _playerHandler.GetPlayers())
        {
            if (4 - pParameter < player.GetComponentInChildren<PlayerStatus>().GetLives())
            {
                _lifes.setValue(pParameter);
            }
        }
    }

    void OnDestroy()
    {
        _bgMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
