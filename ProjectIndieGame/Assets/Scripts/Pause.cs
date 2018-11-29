using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _button;

    private void Start()
    {
        //if (_pauseScreen.activeSelf)
        //{
        //    Time.timeScale = 1;
        //    Paused = true;
        //}
        //else
        //{
        //    Time.timeScale = 0;
        //    Paused = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start_P1") || Input.GetButtonDown("Start_P2") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseScreen.activeSelf)
            {
                Time.timeScale = 1;
                _pauseScreen.SetActive(false);
                Paused = false;
            }
            else
            {

                Time.timeScale = 0;
                _pauseScreen.SetActive(true);
                Paused = true;
                EventSystem.current.SetSelectedGameObject(_button);
            }
        }
    }

    public static bool Paused { get; set; }

    public void Resume()
    {
        Time.timeScale = 1;
        _pauseScreen.SetActive(false);
        Paused = false;
    }
}
