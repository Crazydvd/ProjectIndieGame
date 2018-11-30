using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private GameObject _resolutionScreen;
    [SerializeField] private Text resolutionScreenText;
    [SerializeField] private GameObject _firstButton;

    Rigidbody _rigidbody;
    Vector3 startPosition;
    PlayerStatus _playerStatus;
    PlayersHandler _playersHandler;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
        _playersHandler = Camera.main.GetComponent<PlayersHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToUpper() == "BOUNDARY")
        {
            Debug.Log("boop");
            _playerStatus.DecreaseLives();
            _playerStatus.ResetDamage();

            if (_playerStatus.GetLives() <= 0)
            {
                Debug.Log("You just fucking died");
                _playersHandler.GetPlayers().Remove(transform.parent.gameObject);
                Destroy(gameObject, 0.1f);
                Debug.Log(_playersHandler.GetPlayers().Count);

                //Victory Royal
                if (_playersHandler.GetPlayers().Count == 1)
                {
                    resolutionScreenText.text = _playersHandler.GetPlayers()[0].name + resolutionScreenText.text;
                    _resolutionScreen.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(_firstButton);
                    Time.timeScale = 0;
                }
                return;
            }
            transform.position = startPosition;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
