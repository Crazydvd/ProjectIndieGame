using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _stageSelect;
	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Char_P1", -1);
        PlayerPrefs.SetInt("Char_P2", -1);
    }
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("Char_P1") != -1 && PlayerPrefs.GetInt("Char_P2") != -1)
        {
            _stageSelect.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (Input.GetButtonDown("Decline_P1") && PlayerPrefs.GetInt("Char_P1") == -1)
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
