using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{

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
            _playerStatus.DecreaseLives();
            _playerStatus.ResetDamage();

            if (_playerStatus.GetLives() <= 0)
            {
                _playersHandler.GetPlayers().Remove(transform.parent.gameObject);
                Destroy(gameObject, 0);

                //Victory Royal
                if (_playersHandler.GetPlayers().Count == 1)
                {
                    _playersHandler.EndGame(_playersHandler.GetPlayers()[0].name);
                }
                return;
            }

            transform.position = startPosition;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
