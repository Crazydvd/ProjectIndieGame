using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
    FMOD.Studio.EventInstance _rollSound;
    FMOD.Studio.ParameterInstance _speed;

    Rigidbody _rigidbody;

    private bool _paused;

    void Start()
    {
        _rollSound = FMODUnity.RuntimeManager.CreateInstance("event:/rollingv2");
        _rollSound.getParameter("speed", out _speed);
        _rigidbody = GetComponent<Rigidbody>();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_rollSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        _rollSound.start();
        _rollSound.release();
    }


    void Update()
    {
        float _speedParameter = _rigidbody.velocity.magnitude / 100f;
        
        _speed.setValue(_speedParameter);

        if (Time.timeScale == 0)
        {
            _rollSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _paused = true;
        }
        else if (_paused)
        {
            _rollSound = FMODUnity.RuntimeManager.CreateInstance("event:/rollingv2");
            _rollSound.getParameter("speed", out _speed);
            _rollSound.start();
            _rollSound.release();
            _paused = false;
        }
    }

    void OnDestroy()
    {
        _rollSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
