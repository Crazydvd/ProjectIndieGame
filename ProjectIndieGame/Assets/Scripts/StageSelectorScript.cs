using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectorScript : MonoBehaviour {
    [SerializeField] int _windowSpeed = 1;
    [SerializeField] float _timeoutTime = 0.15f;
    [SerializeField] GameObject _startBanner;

    RectTransform _rectTransform;
    List<GameObject> _stages = new List<GameObject>();

    float _offset;
    float _originOffset;
    float _targetOffset;
    int _index = 0;
    bool _selected = false;
    float _timeoutTimer;

    // Use this for initialization
    void Start()
    {
        // populate list with models
        foreach (Transform gObject in GetComponentInChildren<Transform>())
        {
            if (gObject != gameObject)
            {
                _stages.Add(gObject.gameObject);
            }
        }

        _rectTransform = GetComponent<RectTransform>();
        _originOffset = _rectTransform.transform.localPosition.x;
        _offset = _stages[1].GetComponent<RectTransform>().transform.localPosition.x;


        SetStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (_selected && Input.GetButtonDown("Start_P" + ControllerSettings.player1Joystick))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetButtonDown("Accept_P" + ControllerSettings.player1Joystick))
        {
            PlayerSettings.stageSelected = _index;
            _startBanner.SetActive(true);
            _selected = true;
        }
        else if (Input.GetButtonDown("Decline_P" + ControllerSettings.player1Joystick))
        {
            Invoke("Deselect", 0.001f);
        }

        if (_rectTransform.localPosition.x != _targetOffset)
        {
            MoveWindow(); 
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
            if (Input.GetAxis("LeftHorizontal_P" + ControllerSettings.player1Joystick) > 0)
            {
                _index++;
                if (_index > _stages.Count - 1) _index = 0;
                SetStage();
            }
            if (Input.GetAxis("LeftHorizontal_P" + ControllerSettings.player1Joystick) < 0)
            {
                _index--;
                if (_index < 0) _index = _stages.Count - 1;
                SetStage();
            }

        }
    }

    void Deselect()
    {
        PlayerSettings.stageSelected = -1;
        _startBanner.SetActive(false);
        _selected = false;
    }

    void MoveWindow()
    {
        _rectTransform.localPosition = Vector3.Lerp(_rectTransform.localPosition, new Vector3(_targetOffset, 0, 0), Time.deltaTime * _windowSpeed);
    }

    void SetStage()
    {
        _targetOffset = _originOffset - (_offset * _index);
        _timeoutTimer = _timeoutTime;
    }

    private void OnEnable()
    { 
        if(_rectTransform != null)
        {
            _index = 0;
            _targetOffset = _originOffset;
            _rectTransform.localPosition = new Vector3(_originOffset, 0, 0);
        }
    }
}
