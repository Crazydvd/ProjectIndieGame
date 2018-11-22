using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Force = 10;

    void Update()
    {
        if (Pause.Paused)
        {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("DisableHitBox", 0.25f);
        }
    }

    void DisableHitBox()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
