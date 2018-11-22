using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScript : MonoBehaviour
{
    public Image resolutionScreen;
    Rigidbody _rigidbody;
    Vector3 startPosition;
    PlayerStatus _playerStatus;    

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        _playerStatus = GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToUpper() == "BOUNDARY")
        {
            Debug.Log("boop");
            _playerStatus.DecreaseLives();

            if (_playerStatus.GetLives() <= 0)
            {
                Debug.Log("You just fucking died");
                resolutionScreen.gameObject.SetActive(true);
                Destroy(gameObject, 0f);
                return;
            }
            _rigidbody.velocity = Vector3.zero;
            transform.position = startPosition;
        }
    }
}
