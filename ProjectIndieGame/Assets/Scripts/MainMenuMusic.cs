using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    FMOD.Studio.EventInstance _tutorialMusic;

    private float _masterVolume = 1;
    private bool _fadeOut;
    private bool _fadeIn;

    void Start()
    {
        Rigidbody _rigidbody = GetComponent<Rigidbody>();
        _tutorialMusic = FMODUnity.RuntimeManager.CreateInstance("event:/tutorial");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(_tutorialMusic, transform, _rigidbody);
        _tutorialMusic.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
    }

    void Update()
    {
        if (_fadeOut)
        {
            _masterVolume -= 0.005f;
            if (_masterVolume < 0)
            {
                _masterVolume = 0;
                _fadeOut = false;
            }
            FMODUnity.RuntimeManager.GetVCA("vca:/All sounds").setVolume(_masterVolume);
        }

        if (_fadeIn)
        {
            _masterVolume += 0.005f;
            if (_masterVolume > 1)
            {
                _masterVolume = 1;
                _fadeIn = false;
            }
            FMODUnity.RuntimeManager.GetVCA("vca:/All sounds").setVolume(_masterVolume);
        }

    }

    public void FadeOutMenuMusic()
    {
        _fadeOut = true;
    }

    public void FadeInMenuMusic()
    {
        _fadeIn = true;
    }

    public void EnableTutorialMusic()
    {
        _tutorialMusic.start();
    }

    public void DisableTutorialMusic()
    {
        _tutorialMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    void OnDestroy()
    {
        _tutorialMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
