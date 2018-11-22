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

    GameObject _currentSelector;
    List<GameObject> _characters = new List<GameObject>();
    List<GameObject> _models = new List<GameObject>();

    int _index = 0;
    bool _selected = false;
    float _timeoutTimer;

    // Use this for initialization
    void Start()
    {
        // populate list with portraits
        foreach (Transform gObject in gameObject.GetComponentInChildren<Transform>())
        {
            if (gObject != gameObject)
            {
                _characters.Add(gObject.gameObject);
            }
        }

        // populate list with models
        foreach (Transform gObject in _modelsParent.GetComponentInChildren<Transform>())
        {
            if (gObject != gameObject)
            {
                _models.Add(gObject.gameObject);
            }
        }

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
            PlayerPrefs.SetInt("Char_P" + _playerID, _index);
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

        // character selection
        if (!_selected)
        {
            if (Input.GetAxis("LeftHorizontal_P" + _playerID) > 0)
            {
                _index++;
                if (_index > _characters.Count - 1) _index = 0;
                SetCharacter();
            }
            if (Input.GetAxis("LeftHorizontal_P" + _playerID) < 0)
            {
                _index--;
                if (_index < 0) _index = _characters.Count - 1;
                SetCharacter();
            }

        }
    }

    void SetCharacter()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            if (i == _index)
            {
                _currentSelector.transform.position = _characters[i].transform.position;
                _models[i].SetActive(true);
            }
            else
            {
                _models[i].SetActive(false);
            }
        }
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
