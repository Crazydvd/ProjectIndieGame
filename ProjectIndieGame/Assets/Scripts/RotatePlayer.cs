using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    void Update()
    {
        if (Pause.Paused)
        {
            return;
        }

        if (Input.GetJoystickNames().Length > 0) 
        {
            float horizontalInput = Input.GetAxis("RightHorizontal_P1");
            float verticalInput = Input.GetAxis("RightVertical_P1");

            Vec2 direction = new Vec2(horizontalInput, verticalInput);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (horizontalInput != 0 || verticalInput != 0)
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
