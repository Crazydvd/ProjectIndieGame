using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance _mainMenuSound;

    void Start()
    {
        _mainMenuSound = FMODUnity.RuntimeManager.CreateInstance("event:/menu");
        //_mainMenuSound = FMODUnity.RuntimeManager.CreateInstance("event:/menu3D");
        //FMODUnity.RuntimeManager.AttachInstanceToGameObject(_mainMenuSound, transform, GetComponent<Rigidbody>());
        _mainMenuSound.start();
        _mainMenuSound.release();
    }

    void OnDestroy()
    {
        _mainMenuSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
