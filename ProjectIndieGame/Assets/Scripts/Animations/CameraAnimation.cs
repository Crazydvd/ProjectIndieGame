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
        _canvas.GetComponent<CountdownScript>().StartTimer();
        _animator.Play("CameraMove");
    }

    public void PlayInverseMoveAnimation(GameObject pResolutionScreen)
    {
        _resolutionScreen = pResolutionScreen;
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
        Finished = true;

        toggleHUD();
        togglePlayerRotationAndMovement();
    }

    public void StartTimer()
    {
        _canvas.GetComponent<CountdownScript>().StartTimer();
    }

    public static bool Finished { get; set; }
}
