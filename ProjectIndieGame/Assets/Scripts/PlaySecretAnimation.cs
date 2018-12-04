using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaySecretAnimation : MonoBehaviour
{

    [SerializeField] GameObject _sword;
    [SerializeField] Image _white;
    private ScreenShake _screenShake;
    private FMOD.Studio.EventInstance _doomMusic;

    float masterVolume = 1;
    bool activated;
    bool turnWhite = false;

    void Start()
    {
        _screenShake = Camera.main.GetComponent<ScreenShake>();
        _doomMusic = FMODUnity.RuntimeManager.CreateInstance("event:/doom");
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.F12))
        {
            animator.enabled = true;
            activated = true;
            Invoke("ActivateSword", 4.5f);
            _doomMusic.start();
            _doomMusic.release();
        }

        if (activated)
        {
            masterVolume -= 0.005f;
            if (masterVolume < 0)
                masterVolume = 0;

            FMODUnity.RuntimeManager.GetVCA("vca:/All sounds").setVolume(masterVolume);
            _doomMusic.setVolume(1 - masterVolume);
        }

        if (turnWhite)
        {
            Color newcolor = new Color(_white.color.r, _white.color.g, _white.color.b, _white.color.a + 0.004f);
            _white.color = newcolor;

            if (_white.color.a >= 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void ActivateSword()
    {
        _sword.SetActive(true);
        Invoke("ActivateWhite", 2f);
        _sword.GetComponent<Animator>().enabled = true;
        StartCoroutine(_screenShake.Shake(6f, 0.2f));
    }

    void ActivateWhite()
    {
        turnWhite = true;
    }

    void OnDestroy()
    {
        _doomMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FMODUnity.RuntimeManager.GetVCA("vca:/All sounds").setVolume(1);
    }
}
