using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _characterSelect;


    public void StartCamera()
    {
        _mainMenu.SetActive(false);
        GetComponent<Animator>().enabled = true;
    }

    public void EnableCharacterSelect()
    {
        _characterSelect.SetActive(true);
    }
}
