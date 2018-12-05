using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _played = false;

    private GameObject[] _players;
    private GameObject _canvas;
    private GameObject _resolutionScreen;

    public GameObject[] _prefabs;

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
            Transform Body = player.transform.Find("Body");

            if (Body == null)
            {
                continue;
            }

            string material = Body.GetComponent<LoadPlayerSettings>().Material;

            PlayerParameters parameters = player.GetComponent<PlayerParameters>();
            int ID = PlayerPrefs.GetInt("Char_P" + parameters.PLAYER);
            int altID = PlayerPrefs.GetInt("Char_color_P" + parameters.PLAYER);

            GameObject prefab = null;

            if (ID == 0) //Ram
            {
                switch (altID)
                {
                    case 0: //Normal
                        break;
                    case 1: //Blue
                        break;
                    case 2: //Green
                        break;
                    case 3: //Orange
                        break;
                }
            }
            else if (ID == 1) //Bull
            {
                switch (altID)
                {
                    case 0: //Normal
                        break;
                    case 1: //Blue
                        break;
                    case 2: //Green
                        break;
                    case 3: //Orange
                        break;
                }
            }
            else if (ID == 2) //Pig
            {
                switch (altID)
                {
                    case 0: //Normal
                        break;
                    case 1: //Blue
                        break;
                    case 2: //Green
                        break;
                    case 3: //Orange
                        break;
                }
            }
            else
            {
                return;
            }

            Instantiate(prefab, transform.position, transform.rotation);
            player.transform.position = GameObject.Find("Barn").transform.position;
        }

        _animator.Play("CameraMove Inverse");
    }

    public void ShowEndScreen()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove Inverse"))
        {
            togglePlayerRotationAndMovement();
            _resolutionScreen.SetActive(true);

            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().Play("");
        }
    }

    public void EndOfAnimation()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove"))
        {
            Finished = true;            
            toggleHUD();
            GetComponent<SetToPosition>().SetOriginalPosition();
            togglePlayerRotationAndMovement();
            _animator.enabled = false;
        }
    }

    public void StartTimer()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CameraMove"))
            _canvas.GetComponent<CountdownScript>().StartTimer();
    }

    public static bool Finished { get; set; }
}
