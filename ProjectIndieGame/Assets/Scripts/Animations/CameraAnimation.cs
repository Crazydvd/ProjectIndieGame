using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _played = false;

    private GameObject[] _players;
    private GameObject _canvas;

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
        _canvas.SetActive(!_canvas.activeInHierarchy);
    }

    private void togglePlayerRotationAndMovement()
    {
        foreach (GameObject player in _players)
        {
            player.GetComponentInChildren<Movement>().enabled = !player.GetComponentInChildren<Movement>().isActiveAndEnabled;
            player.GetComponentInChildren<RotatePlayer>().enabled = !player.GetComponentInChildren<RotatePlayer>().isActiveAndEnabled;
        }
    }

    private void playMoveAnimation()
    {
        _animator.Play("CameraMove");
    }

    public void EndOfAnimation()
    {
        Finished = true;

        toggleHUD();
        togglePlayerRotationAndMovement();
    }

    public static bool Finished { get; set; }
}
