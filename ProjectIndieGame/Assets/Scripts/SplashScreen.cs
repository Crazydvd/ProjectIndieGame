using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
