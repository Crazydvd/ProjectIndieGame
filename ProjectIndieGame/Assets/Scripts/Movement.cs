using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _speed = 0.2f;

    private Vector3 forward;
    private Vector3 backward;
    private Vector3 left;
    private Vector3 right;

    private Vec2 _velocity;
    private Vec2 _normal;


    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        forward = new Vector3(0, 0, _speed);
        backward = new Vector3(0, 0, -_speed);
        left = new Vector3(-_speed, 0, 0);
        right = new Vector3(_speed, 0, 0);

        _velocity = Vec2.zero;
        _normal = Vec2.zero;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rigidBody.AddForce(forward, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _rigidBody.AddForce(backward, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddForce(left, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _rigidBody.AddForce(right, ForceMode.VelocityChange);
        }

        if (_rigidBody.velocity.magnitude > _speed)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized;
            _rigidBody.velocity = _rigidBody.velocity * _speed;
        }
    }

    private void FixedUpdate()
    {
        _velocity.SetXY(_rigidBody.velocity.x, _rigidBody.velocity.z);
    }

    private void LateUpdate()
    {
        _rigidBody.velocity *= 0;
    }

    private void reflect(Vector3 pNormal)
    {
        _normal.SetXY(pNormal.x, pNormal.z);

        _velocity.Reflect(_normal, 1);
        _rigidBody.velocity.Set(_velocity.x, 0, _velocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToUpper() == "WALL")
        {
            print("Before: " + _rigidBody.velocity);
            reflect(collision.contacts[0].normal);
            print("After: " + _rigidBody.velocity);
            print(" ");
        }
    }
}
