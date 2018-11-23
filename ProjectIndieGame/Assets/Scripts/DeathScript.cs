using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{
    public Text resolutionScreen;
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
                _playersHandler.GetPlayers().Remove(gameObject);
                Destroy(gameObject, 0f);

                //Victory Royal
                if (_playersHandler.GetPlayers().Count == 1)
                {
                    resolutionScreen.text = _playersHandler.GetPlayers()[0].transform.parent.name + resolutionScreen.text;
                    resolutionScreen.gameObject.SetActive(true);
                }
                return;
            }
            _rigidbody.velocity = Vector3.zero;
            transform.position = startPosition;
        }
    }
}
