using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuCameraScript : MonoBehaviour {

    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _characterSelect;
    [SerializeField] GameObject _mainMenuButton;

    private Animator _animator;
    private Animation _animation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animation = _animator.GetComponent<Animation>();

        if (PlayerPrefs.GetInt("MainMenu") == 1) {
            Debug.Log("hmmmm");
            _animator.Play("CharacterSelect", -1, 100f);
        }
    }

    private void Update()
    {
        if (Input.GetButton("RightBumper_P1"))
        {
            _animator.speed += 0.03f;
        }
        else
        {
            _animator.speed = 1f;
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

    public void EnableMainMenu()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("CharacterSelect Reverse"))
        {
            _animator.speed = 1f;
            _mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_mainMenuButton);
        }
    }
}
