using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationScript : MonoBehaviour {

    [SerializeField] private float _stopRotationLimit = 0.1f;
    private Rigidbody _rigidBody;

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_rigidBody.velocity.magnitude < _stopRotationLimit)
        {
            _rigidBody.freezeRotation = true;
        }
        else
        {
            _rigidBody.freezeRotation = false;
        }
	}
}
