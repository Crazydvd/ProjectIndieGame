using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _stageSelect;

    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;

    private MainMenuCameraScript _cameraScript;
    private Vector3 _defaultCharacterRotation = new Vector3(0, 90, 0);

	// Use this for initialization
	void Start () {
        ResetCharacterSelect();

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

    void ResetCharacterSelect()
    {
        PlayerPrefs.SetInt("Char_P1", -1);
        PlayerPrefs.SetInt("Char_P2", -1);
        PlayerPrefs.SetInt("Color_P1", -1);
        PlayerPrefs.SetInt("Color_P2", -1);
        PlayerPrefs.SetInt("Preselect_color_P1", -1);
        PlayerPrefs.SetInt("Preselect_color_P2", -1);
        PlayerPrefs.SetInt("Preselect_char_P1", -1);
        PlayerPrefs.SetInt("Preselect_char_P2", -1);
    }

    private void OnEnable()
    {
        _character1.SetActive(true);
        _character2.SetActive(true);
        ResetCharacterSelect();
    }

    private void OnDisable()
    {
        _character1.transform.localEulerAngles = _defaultCharacterRotation;
        _character2.transform.localEulerAngles = _defaultCharacterRotation;
        _character1.SetActive(false);
        _character2.SetActive(false);
    }
}
