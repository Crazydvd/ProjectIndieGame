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
        _populatePreviewScript = _modelsParent.GetComponent<PopulatePreviewScript>();
        _characterSelectWindowScript = GetComponentInParent<CharacterSelectWindowScript>();

        if (ControllerSettings.player2Joystick != -1 && _controllerID != -1) // check if coming back from stage select (or from the game I suppose)
        {
            DeselectCharacter();
            SelectCharacterToShow();
            PlayerSettings.playersSelected = new bool[] { false, false, false, false };
            return;
        }

        _controllerID = -1;
        _char = 0;

        if (_playerID == 1)
        {
            _controllerID = ControllerSettings.player1Joystick;
            PlayerSettings.takenChararacters[0, 0] = true;
            PlayerSettings.player1 = PlayerSettings.character.GOAT;
            PlayerSettings.player1Alt = 0;
            _color = 0;
        }
        else
        {
            for(int i = 0; i < 4; i++)
            {
                if (PlayerSettings.takenChararacters[0, i] == false)
                {
                    _color = i;
                }
            }
        }

        DeselectCharacter();

        if(_controllerID == -1)
        {
            return;
        }
        SelectCharacterToShow();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            string temp = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    temp += PlayerSettings.takenChararacters[i, j];
                }
            }
        }

        if (PlayerSettings.playerHasLeft[_playerID - 1]) // check if a player has left the game
        {
            revalidateCharacter();
        }

        // Setting controller P2 - P4
        if (_playerID > 1 && _controllerID == -1)
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
            _selected = true;

            SetNameSelected();
            PlayerSettings.playersSelected[_playerID - 1] = true;
            _characterSelectWindowScript.CheckIfAllJoined(true);
        }
        else if (Input.GetButtonDown("Decline_P" + _controllerID))
        {
            if (_selected) // deselect
            {
                Invoke("DeselectCharacter", 0.001f);
            }
            else // leave game
            {
                removePlayerFromGame();
                return;
            }
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
            if (equalCharacters()) return; // don't bother if there is no available color...

            PlayerSettings.takenChararacters[_char, _color] = false;
            _color++;
            int potentialColor = _color;
            while (true)
            {
                if (potentialColor > _maxColors - 1) potentialColor = 0;
                if (!PlayerSettings.takenChararacters[_char, potentialColor])
                {
                    _color = potentialColor;
                    break;
                }
                potentialColor++;
            }
            SelectCharacterToShow();
        }
        if (Input.GetButtonDown("LeftBumper_P" + _controllerID))
        {
            if (equalCharacters()) return; // don't bother if there is no available color...

            PlayerSettings.takenChararacters[_char, _color] = false;
            _color--;
            int potentialColor = _color;
            while (true)
            {
                if (potentialColor < 0) potentialColor = _maxColors - 1;
                if (!PlayerSettings.takenChararacters[_char, potentialColor])
                {
                    _color = potentialColor;
                    break;
                }
                potentialColor--;
            }
            SelectCharacterToShow();
        }

        // character selection
        if (!_selected)
        {
            if (Input.GetAxis("LeftHorizontal_P" + _controllerID) > 0)
            {
                PlayerSettings.takenChararacters[_char, _color] = false;
                _char++;
                if (_char > _namesSelected.Length - 1) _char = 0;
                resetColor();
                SelectCharacterToShow();

                SetNameDeselected();
            }
            if (Input.GetAxis("LeftHorizontal_P" + _controllerID) < 0)
            {
                PlayerSettings.takenChararacters[_char, _color] = false;
                _char--;
                if (_char < 0) _char = _namesSelected.Length - 1;
                resetColor();
                SelectCharacterToShow();

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
            DeselectCharacter();
            PlayerSettings.playerHasLeft = new bool[] { true, true, true, true };
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
        int newColor = 0;
        while (true)
        {
            if (!PlayerSettings.takenChararacters[_char, newColor])
            {
                _color = newColor;
                return;                
            }
            newColor++;
        }
    }

    void SetNameDeselected()
    {
        // remove Children
        RemoveChildren(transform);

        Instantiate(_namesDeselected[_char], transform); // initiate new name

        Debug.Log(_joined);

        if (ControllerSettings.listOfPlayers()[_playerID -1] == -1)
        {
            Debug.Log(transform.GetChild(0));
            RemoveChildren(transform);
        }

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

    void SelectCharacterToShow()
    {
        switch (_playerID)
        {
            case 1:
                ShowCharacter(ref PlayerSettings.player1, ref PlayerSettings.player1Alt);
                break;
            case 2:
                ShowCharacter(ref PlayerSettings.player2, ref PlayerSettings.player2Alt);
                break;
            case 3:
                ShowCharacter(ref PlayerSettings.player3, ref PlayerSettings.player3Alt);
                break;
            case 4:
                ShowCharacter(ref PlayerSettings.player4, ref PlayerSettings.player4Alt);
                break;
            default:
                break;
        }
    }

    void ShowCharacter(ref PlayerSettings.character player, ref int skin)
    {
        player = (PlayerSettings.character)_char;
        skin = _color;
        PlayerSettings.takenChararacters[(int)player, skin] = true;


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
        _selected = false;
        PlayerSettings.playersSelected[_playerID - 1] = false;

        SetNameDeselected();
    }

    private void togglePlayerVisuals()
    {

        bool playerEnabled = !_joined;

        for (int i = 0; i < transform.parent.childCount; i++) // disable UI siblings
        {
            GameObject sibling = transform.parent.GetChild(i).gameObject;
            if (sibling != gameObject)
            {
                sibling.SetActive(playerEnabled);
            }
        }

        transform.GetChild(0).gameObject.SetActive(playerEnabled); // disable child

        if (_pressToJoin != null)
        {
            _pressToJoin.SetActive(!playerEnabled); // toggle press to join
        }

        _populatePreviewScript.RemoveModel();

        _joined = playerEnabled;
    }

    //check if there are 4 people with the same character
    bool equalCharacters()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!PlayerSettings.takenChararacters[_char, i])
            {
                return false;
            }
        }
        return true;
    }

    private void checkForControllerInput()
    {
        // list of assigned controllers
        int[] listOfPlayers = ControllerSettings.listOfPlayers();

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
                    SetNameDeselected();
                    togglePlayerVisuals();
                    resetColor();
                    SelectCharacterToShow();
                    break;
                }
            }
        }

        _controllerID = listOfPlayers[_playerID - 1];
    }

    void revalidateCharacter()
    {
        switch (_playerID)
        {
            case 1:
                _char = (int)PlayerSettings.player1;
                _color = PlayerSettings.player1Alt;
                _controllerID = ControllerSettings.player1Joystick;
                break;
            case 2:
                _char = (int)PlayerSettings.player2;
                _color = PlayerSettings.player2Alt;
                _controllerID = ControllerSettings.player2Joystick;
                break;
            case 3:
                _char = (int)PlayerSettings.player3;
                _color = PlayerSettings.player3Alt;
                _controllerID = ControllerSettings.player3Joystick;
                break;
            case 4:
                _char = (int)PlayerSettings.player4;
                _color = PlayerSettings.player4Alt;
                _controllerID = ControllerSettings.player4Joystick;
                break;
        }
        PlayerSettings.playerHasLeft[_playerID -1] = false;
        if (_controllerID == -1 && _joined)
        {
            togglePlayerVisuals();
        }
        else if(_controllerID != -1)
        {
            SelectCharacterToShow();
        }
        if (_selected)
        {
            DeselectCharacter();
        }
    }

    void removePlayerFromGame()
    {
        PlayerSettings.takenChararacters[_char, _color] = false;

        int amountOfPlayers = ControllerSettings.AmountOfPlayers();

        if(amountOfPlayers == 1)
        {
            _characterSelectWindowScript.BackToMenu();
        }

        if (amountOfPlayers == _playerID) // if last joined player
        {
            switch (_playerID)
            {
                case 2:
                    ControllerSettings.player2Joystick = -1;
                    PlayerSettings.player2 = PlayerSettings.character.GOAT;
                    PlayerSettings.player2Alt = -1;
                    break;
                case 3:
                    ControllerSettings.player3Joystick = -1;
                    PlayerSettings.player3 = PlayerSettings.character.GOAT;
                    PlayerSettings.player3Alt = -1;
                    break;
                case 4:
                    ControllerSettings.player4Joystick = -1;
                    PlayerSettings.player4 = PlayerSettings.character.GOAT;
                    PlayerSettings.player4Alt = -1;
                    break;
            }
            _controllerID = -1;
            togglePlayerVisuals();
        }
        else
        {
            if(_playerID == 3) // it ain't pretty, but it's honest code
            {
                ControllerSettings.player3Joystick = ControllerSettings.player4Joystick;
                PlayerSettings.player3 = PlayerSettings.player4;
                PlayerSettings.player3Alt = PlayerSettings.player4Alt;
            }
            if(_playerID == 2)
            {
                // switch over
                ControllerSettings.player2Joystick = ControllerSettings.player3Joystick;
                PlayerSettings.player2 = PlayerSettings.player3;
                PlayerSettings.player2Alt = PlayerSettings.player3Alt;
                ControllerSettings.player3Joystick = ControllerSettings.player4Joystick;
                PlayerSettings.player3 = PlayerSettings.player4;
                PlayerSettings.player3Alt = PlayerSettings.player4Alt;
            }
            if(_playerID == 1)
            {
                ControllerSettings.player1Joystick = ControllerSettings.player2Joystick;
                PlayerSettings.player1 = PlayerSettings.player2;
                PlayerSettings.player1Alt = PlayerSettings.player2Alt;
                ControllerSettings.player2Joystick = ControllerSettings.player3Joystick;
                PlayerSettings.player2 = PlayerSettings.player3;
                PlayerSettings.player2Alt = PlayerSettings.player3Alt;
                ControllerSettings.player3Joystick = ControllerSettings.player4Joystick;
                PlayerSettings.player3 = PlayerSettings.player4;
                PlayerSettings.player3Alt = PlayerSettings.player4Alt;
            }

            switch (amountOfPlayers)
            {
                case 4:
                    ControllerSettings.player4Joystick = -1;
                    PlayerSettings.player4 = PlayerSettings.character.GOAT;
                    PlayerSettings.player4Alt = -1;
                    break;
                case 3:
                    ControllerSettings.player3Joystick = -1;
                    PlayerSettings.player3 = PlayerSettings.character.GOAT;
                    PlayerSettings.player3Alt = -1;
                    break;
                case 2:
                    ControllerSettings.player2Joystick = -1;
                    PlayerSettings.player2 = PlayerSettings.character.GOAT;
                    PlayerSettings.player2Alt = -1;
                    break;
            }
            PlayerSettings.playerHasLeft = new bool[]{ true, true, true, true };
        }
        PlayerSettings.playersSelected = new bool[] { false, false, false, false }; // deselect everyone
    }

    private void OnDisable()
    {
        if (_populatePreviewScript != null)
        {
            _populatePreviewScript.RemoveModel();
        }
    }
}
