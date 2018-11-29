using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{
    public GameObject _resolutionScreen;
    public Text resolutionScreenText;
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
                    resolutionScreenText.gameObject.SetActive(true);
                }
                return;
            }
            transform.position = startPosition;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
