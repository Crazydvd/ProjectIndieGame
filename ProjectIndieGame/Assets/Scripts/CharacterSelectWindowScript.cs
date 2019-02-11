using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _stageSelect;

    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;
    [SerializeField] GameObject _character3;
    [SerializeField] GameObject _character4;


    private MainMenuCameraScript _cameraScript;
    private Vector3 _defaultCharacterRotation = new Vector3(0, 90, 0);

	// Use this for initialization
	void Start () {
        _cameraScript = Camera.main.GetComponent<MainMenuCameraScript>();
    }

    public bool CheckIfAllJoined(bool StageSelect)
    {
        List<int> listOfPlayers = new List<int>() { ControllerSettings.player1Joystick, ControllerSettings.player2Joystick, ControllerSettings.player3Joystick, ControllerSettings.player4Joystick };
        int amountOfPlayers = 0;
        for (int i = 0; i < 4; i++) // check how much players joined
        {
            if (listOfPlayers[i] != -1)
            {
                amountOfPlayers++;
            }
        }
        bool allHaveJoined = true;
        for (int i = 0; i < amountOfPlayers; i++)
        {
            if (!PlayerSettings.playersSelected[i])
            {
                allHaveJoined = false;
            }
        }

        if (allHaveJoined && amountOfPlayers > 1)
        {
            if (StageSelect)
            {
                _stageSelect.SetActive(true);
                this.gameObject.SetActive(false);
            }
            return true;
        }
        return false;
    }

    public void BackToMenu()
    {
        _cameraScript.BackToMain();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //PlayerSettings.ResetTakenCharacters();
    }

    private void OnDisable()
    {
        if (_character1 != null)
        {
            _character1.transform.localEulerAngles = _defaultCharacterRotation;
            _character2.transform.localEulerAngles = _defaultCharacterRotation;
            _character3.transform.localEulerAngles = _defaultCharacterRotation;
            _character4.transform.localEulerAngles = _defaultCharacterRotation;
        }
    }
}
