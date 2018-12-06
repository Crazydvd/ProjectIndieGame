using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    public GameObject[] _prefabs;

    private Animator _animator;
    private bool _played = false;

    private GameObject[] _players;
    private GameObject _canvas;
    private GameObject _resolutionScreen;

    private void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (_canvas == null)
        {
            throw new System.Exception("YOU DIDN'T APPLY A TAG TO THE CANVAS");
        }

        Finished = false;

        _animator = GetComponent<Animator>();

        toggleHUD();
        togglePlayerRotationAndMovement();
        playMoveAnimation();
    }

    private void toggleHUD()
    {
        _canvas.transform.Find("HUD").gameObject.SetActive(!_canvas.transform.Find("HUD").gameObject.activeInHierarchy);
    }

    private void togglePlayerRotationAndMovement()
    {
        foreach (GameObject player in _players)
        {
            if (player.GetComponentInChildren<Movement>() == null)
            {
                continue;
            }

            player.GetComponentInChildren<Movement>().enabled = !player.GetComponentInChildren<Movement>().isActiveAndEnabled;
            player.GetComponentInChildren<RotatePlayer>().enabled = !player.GetComponentInChildren<RotatePlayer>().isActiveAndEnabled;
        }
    }

    private void playMoveAnimation()
    {
        _animator.Play("CameraMove");
    }

    public void PlayInverseMoveAnimation(GameObject pResolutionScreen)
    {
        _resolutionScreen = pResolutionScreen;
        _canvas.GetComponentInChildren<TimerScript>().StopTimer();

        foreach (GameObject player in _players)
        {
            if (GetComponent<PlayersHandler>().WinnerID != player.GetComponent<PlayerParameters>().PLAYER)
            {
                continue;
            }

            PlayerParameters parameters = player.GetComponent<PlayerParameters>();

            int ID = PlayerPrefs.GetInt("Char_P" + parameters.PLAYER);
            int altID = PlayerPrefs.GetInt("Char_color_P" + parameters.PLAYER);

            GameObject prefab = null;

            if (ID == 0) //Ram
            {
                switch (altID)
                {
                    case 0: //Normal
                        prefab = _prefabs[0];
                        break;
                    case 1: //Blue
                        prefab = _prefabs[1];
                        break;
                    case 2: //Green
                        prefab = _prefabs[2];
                        break;
                    case 3: //Orange
                        prefab = _prefabs[3];
                        break;
                }
            }
            else if (ID == 1) //Bull
            {
                switch (altID)
                {
                    case 0: //Normal
                        prefab = _prefabs[4];
                        break;
                    case 1: //Blue
                        prefab = _prefabs[5];
                        break;
                    case 2: //Green
                        prefab = _prefabs[6];
                        break;
                    case 3: //Orange
                        prefab = _prefabs[7];
                        break;
                }
            }
            else if (ID == 2) //Pig
            {
                switch (altID)
                {
                    case 0: //Normal
                        prefab = _prefabs[8];
                        break;
                    case 1: //Blue
                        prefab = _prefabs[9];
                        break;
                    case 2: //Green
                        prefab = _prefabs[10];
                        break;
                    case 3: //Orange
                        prefab = _prefabs[11];
                        break;
                }
            }
            else
            {
                return;
            }
            GameObject winPosition = GameObject.Find("WinPosition");

            GameObject winner = Instantiate(prefab, winPosition.transform);

            player.transform.position = GameObject.Find("Barn").transform.position;
        }

        toggleHUD();
        _animator.Play("CameraMove Inverse");
    }

    public void ShowEndScreen()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove Inverse"))
        {
            togglePlayerRotationAndMovement();

            _resolutionScreen.SetActive(true);
        }
    }

    public void EndOfAnimation()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove"))
        {
            Finished = true;
            _animator.enabled = false;
            Camera.main.GetComponent<SetToPosition>().SetOriginalPosition();
            toggleHUD();
            togglePlayerRotationAndMovement();
        }
    }

    public void StartTimer()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove"))
            _canvas.GetComponent<CountdownScript>().StartTimer();
    }

    public static bool Finished { get; set; }
}
