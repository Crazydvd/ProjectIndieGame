using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxScript : MonoBehaviour
{
    ScreenShake screenShake;

    void Start()
    {
        screenShake = Camera.main.GetComponent<ScreenShake>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("I hit ur ass");
            StartCoroutine(screenShake.Shake(0.2f, 0.1f));
        }
    }   
}
