using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _characterSelect;
    [SerializeField] GameObject _stageSelect;

    int _menuLevel;

	// Use this for initialization
	void Start () {
        _menuLevel = PlayerPrefs.GetInt("MainMenu");

        if (_menuLevel == 1)
        {
            PlayerPrefs.SetInt("Stage", -1);
            PlayerPrefs.SetInt("Char_P1", -1);
            PlayerPrefs.SetInt("Char_P2", -1);
            _mainMenu.SetActive(false);
            _characterSelect.SetActive(true);
        }
        if(_menuLevel == 2)
        {
            PlayerPrefs.SetInt("Stage", -1);
            _mainMenu.SetActive(false);
            _stageSelect.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Stage", -1);
            PlayerPrefs.SetInt("Char_P1", -1);
            PlayerPrefs.SetInt("Char_P2", -1);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
