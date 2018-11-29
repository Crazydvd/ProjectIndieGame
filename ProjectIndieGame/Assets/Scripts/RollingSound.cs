using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingSound : MonoBehaviour
{
    FMOD.Studio.EventInstance _rollSound;
    FMOD.Studio.ParameterInstance _speed;

    Rigidbody _rigidbody;

    void Awake()
    {
        _rollSound = FMODUnity.RuntimeManager.CreateInstance("event:/rolling");
        _rollSound.getParameter("speed", out _speed);
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_rollSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        _rollSound.start();
    }


    void Update()
    {
        float _speedParameter = _rigidbody.velocity.magnitude / 100f;
        
        _speed.setValue(_speedParameter);
    }
}
