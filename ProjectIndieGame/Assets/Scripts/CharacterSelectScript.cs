using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    [SerializeField] GameObject _modelsParent;
    [SerializeField] GameObject _selector;
    [SerializeField] GameObject _selectedSelector;
    [Range(1,2)]
    [SerializeField] int _playerID = 1;
    [SerializeField] float _timeoutTime = 0.2f;

    PopulatePreviewScript _populatePreviewScript;
    GameObject _currentSelector;
    List<GameObject> _characters = new List<GameObject>();

    int _char = 0;
    int _color = 0;
    int _maxColors = 4;
    int _enemyPlayerID;
    bool _selected = false;
    float _timeoutTimer;

    // Use this for initialization
    void Start()
    {
        _populatePreviewScript = _modelsParent.GetComponent<PopulatePreviewScript>();
        _enemyPlayerID = _playerID == 1 ? 2 : 1;

        // populate list with portraits
        foreach (Transform gObject in gameObject.GetComponentInChildren<Transform>())
        {
            if (gObject != gameObject)
            {
                _characters.Add(gObject.gameObject);
            }
        }

        _color = _playerID - 1;

        int oldSelect = PlayerPrefs.GetInt("Char_P" + _playerID);
        if (oldSelect != -1)
        {
            _char = oldSelect;
        }
        PlayerPrefs.SetInt("Preselect_char_P" + _playerID, _char);
        PlayerPrefs.SetInt("Preselect_color_P" + _playerID, _color);

        _currentSelector = Instantiate(_selector, transform);
        SetCharacter();
    }

    // Update is called once per frame
    void Update()
    { 
        // character rotation
        if (Input.GetAxis("RightHorizontal_P" + _playerID) > 0) {
            _modelsParent.transform.Rotate(0, -1, 0);
        }
        if (Input.GetAxis("RightHorizontal_P" + _playerID) < 0)
        {
            _modelsParent.transform.Rotate(0, 1, 0);
        }

        if (Input.GetButtonDown("Accept_P" + _playerID))
        {
            _currentSelector.gameObject.GetComponent<Image>().sprite = _selectedSelector.GetComponent<Image>().sprite;
            PlayerPrefs.SetInt("Char_P" + _playerID, _char);
            _selected = true;
        }
        else if (Input.GetButtonDown("Decline_P" + _playerID))
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
        if(Input.GetButtonDown("RightBumper_P" + _playerID))
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
        if (Input.GetButtonDown("LeftBumper_P" + _playerID))
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
            if (Input.GetAxis("LeftHorizontal_P" + _playerID) > 0)
            {    
                _char++;
                if (_char > _characters.Count - 1) _char = 0;
                resetColor();
                SetCharacter();
            }
            if (Input.GetAxis("LeftHorizontal_P" + _playerID) < 0)
            {
                _char--;
                if (_char < 0) _char = _characters.Count - 1;
                resetColor();
                SetCharacter();
            }
        }
    }

    void resetColor()
    {
        Debug.Log(PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) + " - " + PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID));
        Debug.Log(_char + " - " + _color);
        _color = 0;
        if (PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) == _char && PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID) == _color)
        {
            Debug.Log(PlayerPrefs.GetInt("Preselect_char_P" + _enemyPlayerID) + " - " + PlayerPrefs.GetInt("Preselect_color_P" + _enemyPlayerID));
            Debug.Log("heh");
            _color++;
        }
    }

    void SetCharacter()
    {
        _currentSelector.transform.position = _characters[_char].transform.position;
        PlayerPrefs.SetInt("Preselect_color_P" + _playerID, _color);
        PlayerPrefs.SetInt("Preselect_char_P" + _playerID, _char);

        _populatePreviewScript.SetModel(_char, _color);

        _timeoutTimer = _timeoutTime;
    }

    void DeselectCharacter() {
        PlayerPrefs.SetInt("Char_P" + _playerID, -1);
        _currentSelector.gameObject.GetComponent<Image>().sprite = _selector.GetComponent<Image>().sprite;
        _selected = false;
    }

    private void OnEnable()
    {
        _selected = false;
        PlayerPrefs.SetInt("Char_P" + _playerID, -1);
        if (_currentSelector != null)
        {
            _currentSelector.gameObject.GetComponent<Image>().sprite = _selector.GetComponent<Image>().sprite;
        }
    }
}
