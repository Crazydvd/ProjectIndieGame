﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    [SerializeField] float _deadzone = 0.25f;

    private int[] listOfPlayers = new int[] { ControllerSettings.player1Joystick, ControllerSettings.player2Joystick, ControllerSettings.player3Joystick, ControllerSettings.player4Joystick };

    private PlayerParameters _parameters;

    private void Start()
    {
        _parameters = transform.root.GetComponent<PlayerParameters>();
    }


    void Update()
    {
        if (Pause.Paused)
        {
            return;
        }

        if (listOfPlayers[_parameters.PLAYER - 1] != 0) 
        {

            Vector2 stickInput = new Vector2(Input.GetAxis("RightHorizontal_P" + listOfPlayers[_parameters.PLAYER - 1]), Input.GetAxis("RightVertical_P" + listOfPlayers[_parameters.PLAYER - 1]));
            if (stickInput.magnitude < _deadzone)
                stickInput = Vector2.zero;

            Vec2 direction = new Vec2(stickInput.x, stickInput.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (stickInput.x != 0 || stickInput.y != 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
        else
        {
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = Input.mousePosition - position;
            float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }
}
