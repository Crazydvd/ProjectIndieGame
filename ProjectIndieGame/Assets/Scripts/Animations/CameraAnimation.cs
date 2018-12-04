using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private Animator _animator;
    private bool _played = false;

    public static GameObject Camera1;
    public static GameObject Camera2;

    private GameObject[] _players;
    private GameObject _canvas;

    private void Start()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        _players = GameObject.FindGameObjectsWithTag("Player");
        _canvas = GameObject.FindGameObjectWithTag("Canvas");

        Finished = false;

        foreach (GameObject camera in cameras)
        {
            if (camera == gameObject)
            {
                continue;
            }
            Camera1 = camera;
        }

        Camera1.SetActive(false);
        _animator = GetComponent<Animator>();
        Camera2 = gameObject;

        toggleHUD();
        togglePlayerRotationAndMovement();
        playMoveAnimation();
    }

    private void Update()
    {
        AnimatorStateInfo currentState = _animator.GetCurrentAnimatorStateInfo(0);

        if (currentState.IsName("CameraMove"))
        {
            _played = true;
            return;
        }

        if (!_played)
        {
            return;
        }

        if (currentState.IsName("idle"))
        {
            Finished = true;
            ToggleCameras();
            toggleHUD();
            togglePlayerRotationAndMovement();
        }
    }

    public static void ToggleCameras()
    {
        Camera1.SetActive(!Camera1.activeSelf);
        Camera2.SetActive(!Camera2.activeSelf);
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

    public static bool Finished { get; set; }
}
