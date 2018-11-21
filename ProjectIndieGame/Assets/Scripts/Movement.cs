using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private float _speed = 0.2f;

    private Vector3 forward;
    private Vector3 backward;
    private Vector3 left;
    private Vector3 right;

    // Use this for initialization
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        forward = new Vector3(0, 0, _speed);
        backward = new Vector3(0, 0, -_speed);
        left = new Vector3(-_speed, 0, 0);
        forward = new Vector3(_speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidBody.velocity += forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidBody.velocity += backward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidBody.velocity += left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidBody.velocity += right;
        }
    }
}
