using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start_P1") || Input.GetButtonDown("Start_P2"))
        {
            if (_pauseScreen.activeSelf)
            {
                print("UNPAUSED???");
                Time.timeScale = 1;
                _pauseScreen.SetActive(false);
                Paused = false;
            }
            else
            {
                print("PAUSED!!!");
                Time.timeScale = 0;
                _pauseScreen.SetActive(true);
                Paused = true;
            }
        }
    }

    public static bool Paused { get; set; }
}
