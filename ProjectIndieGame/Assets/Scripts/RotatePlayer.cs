using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    void Update()
    {       
        Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = Input.mousePosition - position;
        float angle = -Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
}
