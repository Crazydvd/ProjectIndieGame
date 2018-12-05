using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayersHandler : MonoBehaviour
{

    public List<GameObject> players = new List<GameObject>();

    [SerializeField] private GameObject _resolutionScreen;
    [SerializeField] private Text resolutionScreenText;
    [SerializeField] private GameObject _firstButton;

    private BackgroundMusic _bgMusic;

    void Start()
    {
        _bgMusic = Camera.main.GetComponent<BackgroundMusic>();
    }

    public List<GameObject> GetPlayers()
    {
        return players;
    }

    public void EndGame(string pWinner)
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<CameraAnimation>().PlayInverseMoveAnimation(_resolutionScreen);
        resolutionScreenText.text = pWinner + resolutionScreenText.text;
        EventSystem.current.SetSelectedGameObject(_firstButton);
        //Time.timeScale = 0;
        _bgMusic.StopMusic();
        FMODUnity.RuntimeManager.PlayOneShot("event:/end game");
    }
}
