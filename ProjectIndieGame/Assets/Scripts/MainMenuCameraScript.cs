using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class MainMenuCameraScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _characterSelect;
    [SerializeField] GameObject _mainMenuButton;
    [SerializeField] GameObject _boomBox;
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] VideoClip _videoClip;

    private Animator _animator;
    private Animation _animation;
    private VideoClip _originalClip;
    private MainMenuMusic _mainMenuMusic;
    private bool _tutorialPlaying = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animation = _animator.GetComponent<Animation>();
        _mainMenuMusic = _boomBox.GetComponent<MainMenuMusic>();

        if (PlayerPrefs.GetInt("MainMenu") == 1) {
            _animator.enabled = true;
            _animator.Play("CharacterSelect", 0, 1f);
        }

        _originalClip = _videoPlayer.clip;
    }

    private void Update()
    {
        if (Input.GetButton("RightBumper_P1") || Input.GetKey(KeyCode.Space))
        {
            _animator.speed += 0.03f;
        }
        else
        {
            _animator.speed = 1f;
        }

        if ((Input.GetButtonDown("Decline_P1") || Input.GetKeyDown(KeyCode.Escape)) && _tutorialPlaying == true)
        {
            _mainMenuMusic.DisableTutorialMusic();
            _mainMenuMusic.FadeInMenuMusic();
            _tutorialPlaying = false;
            _animator.Play("Controls Inverse");
            _videoPlayer.clip = _originalClip;
        }
    }

    public void StartCamera()
    {
        _mainMenu.SetActive(false);
        _animator.enabled = true;
        _animator.Play("CharacterSelect");
    }

    public void BackToMain()
    {
        _animator.Play("CharacterSelect Reverse");
    }

    public void EnableCharacterSelect()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterSelect"))
        {
            _animator.speed = 1f;
            _characterSelect.SetActive(true);
        }
    }

    public void StartTutorial()
    {
        _mainMenu.SetActive(false);
        _animator.enabled = true;
        _animator.Play("Controls");
        _mainMenuMusic.FadeOutMenuMusic();
    }

    public void EnableTutorial()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Controls"))
        {
            _videoPlayer.GetComponent<MeshRenderer>().material.color = Color.white;
            _animator.speed = 1f;
            _videoPlayer.clip = _videoClip;
            _videoPlayer.Play();
            _tutorialPlaying = true;
            _mainMenuMusic.EnableTutorialMusic();
        }
    }

    public void CloseTutorial()
    {      
        _animator.Play("Controls Inverse");
    }

    public void EnableMainMenu()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterSelect Reverse") || _animator.GetCurrentAnimatorStateInfo(0).IsName("Controls Inverse"))
        {
            _animator.speed = 1f;
            _mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_mainMenuButton);
        }
    }
}
