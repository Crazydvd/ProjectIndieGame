using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _stageSelect;

    private MainMenuCameraScript _cameraScript;

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("Char_P1", -1);
        PlayerPrefs.SetInt("Char_P2", -1);
        PlayerPrefs.SetInt("Color_P1", -1);
        PlayerPrefs.SetInt("Color_P2", -1);

        _cameraScript = Camera.main.GetComponent<MainMenuCameraScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt("Char_P1") != -1 && PlayerPrefs.GetInt("Char_P2") != -1)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetButtonDown("Decline_P1") && PlayerPrefs.GetInt("Char_P1") == -1)
        {
            _cameraScript.BackToMain();
            gameObject.SetActive(false);
        }
	}
}
