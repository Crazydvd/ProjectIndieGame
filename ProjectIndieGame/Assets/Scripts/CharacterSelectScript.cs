using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    [SerializeField] GameObject _modelsParent;
    [SerializeField] GameObject _statsParent;

    [SerializeField] GameObject[] _namesDeselected;
    [SerializeField] GameObject[] _namesSelected;
    [SerializeField] GameObject[] _statsOfSelected;

    [SerializeField] GameObject _selectSymbol;
    [SerializeField] GameObject _deselectSymbol;
    [SerializeField] GameObject _backSymbol;

    [SerializeField] GameObject _pressToJoin;

    [Range(1, 4)]
    [SerializeField] int _playerID = 1;
    [SerializeField] float _timeoutTime = 0.2f;

    PopulatePreviewScript _populatePreviewScript;
    CharacterSelectWindowScript _characterSelectWindowScript;

    int _controllerID = -1;
    int _char = 0;
    int _color = 0;
    int _maxColors = 4;
    int _enemyPlayerID;
    bool _selected = false;
    bool _joined = false;
    float _timeoutTimer;

    float _returnTimer;
    float _returnTimerLimit = 2f;

    // Use this for initialization
    void OnEnable()
    {
        _controllerID = -1;

        if (_playerID == 1)
        {
            _controllerID = ControllerSettings.player1Joystick;
        }

        _populatePreviewScript = _modelsParent.GetComponent<PopulatePreviewScript>();
        _characterSelectWindowScript = GetComponentInParent<CharacterSelectWindowScript>();

        _color = _playerID - 1;

        int oldSelect = PlayerPrefs.GetInt("Char_P" + _playerID);
        if (oldSelect != -1)
        {
            _char = oldSelect;
        }

        if(_controllerID == -1)
        {
            return;
        }
        PlayerPrefs.SetInt("Preselect_char_P" + _playerID, _char);
        PlayerPrefs.SetInt("Preselect_color_P" + _playerID, _color);
        SetCharacter();
        SetNameDeselected();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting controller P2 - P4
        if(_playerID > 1 && _controllerID == -1)
        {
            checkForControllerInput();
            return;
        }

        // character rotation
        if (Input.GetAxis("RightHorizontal_P" + _controllerID) > 0)
        {
            _modelsParent.transform.Rotate(0, -1, 0);
        }
        if (Input.GetAxis("RightHorizontal_P" + _controllerID) < 0)
        {
            _modelsParent.transform.Rotate(0, 1, 0);
        }

        // selecting character
        if (Input.GetButtonDown("Accept_P" + _controllerID))
        {
            PlayerPrefs.SetInt("Char_color_P" + _controllerID, _color);
            PlayerPrefs.SetInt("Char_P" + _controllerID, _char);
            _selected = true;

            SetNameSelected();
        }
        else if (Input.GetButtonDown("Decline_P" + _controllerID))
        {
            Invoke("DeselectCharacter", 0.001f);
        }

        // selection timeout
        if (_timeoutTimer > 0f)
        {
            _timeoutTimer -= Time.deltaTime;
            return;
        }

        // change skins
        if (Input.GetButtonDown("RightBumper_P" + _controllerID))
        {
            _color++;
            if (_color > _maxColors - 1) _color = 0;
            if (PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) == _char && PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID) == _color)
            {
                _color++;
            }
            if (_color > _maxColors - 1) _color = 0;
            SetCharacter();
        }
        if (Input.GetButtonDown("LeftBumper_P" + _controllerID))
        {
            _color--;
            if (_color < 0) _color = _maxColors - 1;
            if (PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) == _char && PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID) == _color)
            {
                _color--;
            }
            if (_color < 0) _color = _maxColors - 1;
            SetCharacter();
        }

        // character selection
        if (!_selected)
        {
            if (Input.GetAxis("LeftHorizontal_P" + _controllerID) > 0)
            {
                _char++;
                if (_char > _namesSelected.Length - 1) _char = 0;
                resetColor();
                SetCharacter();

                SetNameDeselected();
            }
            if (Input.GetAxis("LeftHorizontal_P" + _controllerID) < 0)
            {
                _char--;
                if (_char < 0) _char = _namesSelected.Length - 1;
                resetColor();
                SetCharacter();

                SetNameDeselected();
            }
        }
        // Return

        if (Input.GetButton("Decline_P" + _controllerID))
        {
            _returnTimer += Time.deltaTime;
        }

        if(_returnTimer >= _returnTimerLimit)
        {
            RemoveChildren(transform);
            _returnTimer = 0f;
            _characterSelectWindowScript.BackToMenu();
        }

        if(Input.GetButtonUp("Decline_P" + _controllerID))
        {
            _returnTimer = 0f;
        }
    }

    void resetColor()
    {
        _color = 0;
        if (PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) == _char && PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID) == _color)
        {
            _color++;
        }
    }

    void SetNameDeselected()
    {
        // remove Children
        RemoveChildren(transform);
        Instantiate(_namesDeselected[_char], transform); // initiate new name

        // switch button symbols
        _selectSymbol.SetActive(true);
        _deselectSymbol.SetActive(false);

        if (_backSymbol != null && _playerID == 1)
        {
            _backSymbol.SetActive(true);
        }
    }

    void SetNameSelected()
    {
        // remove Children
        RemoveChildren(transform);
        Instantiate(_namesSelected[_char], transform); // initiate new name

        // switch button symbols
        _selectSymbol.SetActive(false);
        _deselectSymbol.SetActive(true);
        if (_backSymbol != null && _playerID == 1)
        {
            _backSymbol.SetActive(false);
        }
    }

    void SetCharacter()
    {
        PlayerPrefs.SetInt("Preselect_color_P" + _playerID, _color);
        PlayerPrefs.SetInt("Preselect_char_P" + _playerID, _char);

        if (_selected)
        {
            PlayerPrefs.SetInt("Char_color_P" + _playerID, _color);
        }

        //showing stats
        RemoveChildren(_statsParent.transform);
        Instantiate(_statsOfSelected[_char], _statsParent.transform);

        _populatePreviewScript.SetModel(_char, _color);

        _timeoutTimer = _timeoutTime;
    }

    void RemoveChildren(Transform parent)
    {
        for (int i = parent.childCount; i > 0; i--)
        {
            Destroy(parent.GetChild(i - 1).gameObject);
        }
    }

    void DeselectCharacter()
    {
        PlayerPrefs.SetInt("Char_P" + _playerID, -1);
        PlayerPrefs.SetInt("Char_color_P" + _playerID, -1);
        _selected = false;

        SetNameDeselected();
    }

    private void checkForControllerInput()
    {
        bool joystick = false;
        // list of assigned controllers

        List<int> listOfPlayers = new List<int>(){ ControllerSettings.player1Joystick, ControllerSettings.player2Joystick, ControllerSettings.player3Joystick, ControllerSettings.player4Joystick };

        for (int i = 0; i < 5; i++) // check inputs
        {
            if (Input.GetButton("Accept_P" + i)) // found input
            {
                if (listOfPlayers[_playerID - 2] == -1)
                { // leave if lower player is not yet set
                    return;
                }

                bool used = false;
                for (int j = 0; j < 4; j++) // check if used by other player
                {
                    if(listOfPlayers[j] == i)
                    {
                        used = true;
                    }
                }
                if (!used) // set controller to player
                {
                    //listOfPlayers[_playerID - 1] = i;
                    switch (_playerID)
                    {
                        case 2:
                            ControllerSettings.player2Joystick = i;
                            break;
                        case 3:
                            ControllerSettings.player3Joystick = i;
                            break;
                        case 4:
                            ControllerSettings.player4Joystick = i;
                            break;
                    }
                    joystick = true;
                    togglePlayerVisuals();
                    SetCharacter();
                    SetNameDeselected();
                    break;
                }
            }
        }

        /*
        string the = "";
        foreach (int numb in ControllerSettings.ListofPlayers())
        {
            the += numb + " - ";
        }
        Debug.Log(the);*/

        _controllerID = listOfPlayers[_playerID - 1];
    }

    private void togglePlayerVisuals()
    {
        enabled = !_joined;

        for (int i = 0; i < transform.parent.childCount; i++) // disable UI siblings
        {
            GameObject sibling = transform.parent.GetChild(i).gameObject;
            if(sibling != gameObject)
            {
                sibling.SetActive(enabled);
            }
        }

        transform.GetChild(0).gameObject.SetActive(enabled); // disable child

        _pressToJoin.SetActive(!enabled); // toggle press to join
        
        _joined = !_joined;
    }

    /*
    private void OnEnable()
    {
        _populatePreviewScript = _modelsParent.GetComponent<PopulatePreviewScript>();
        _enemyPlayerID = _playerID == 1 ? 2 : 1;

        _char = 0;
        _color = _playerID - 1;

        SetCharacter();
        SetNameDeselected();
    }*/
}
