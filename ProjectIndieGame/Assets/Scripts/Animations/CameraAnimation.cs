using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _played = false;
    private bool _done;

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

    private void Update()
    {
        if (_done) return;
        
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("CameraMove"))
        {
            _played = true;
            return;
        }
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
        _done = true;

        toggleHUD();
        togglePlayerRotationAndMovement();
    }

    public static bool Finished { get; set; }
}
