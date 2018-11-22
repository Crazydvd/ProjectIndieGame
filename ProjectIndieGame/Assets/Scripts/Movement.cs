using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    [SerializeField] private float _speed = 0.2f;
    public bool CANTMOVE = false;

    private Vector3 forward;
    private Vector3 backward;
    private Vector3 left;
    private Vector3 right;

    private Vector3 _walkVelocity;
    private Vector3 _flyVelocity;

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
        if (CANTMOVE || Pause.Paused)
        {
            return;
        }

        _walkVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _walkVelocity += forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _walkVelocity += backward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _walkVelocity += left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _walkVelocity += right;
        }

        if (_walkVelocity.magnitude > _speed)
        {
            _walkVelocity = _walkVelocity.normalized;
            _walkVelocity = _walkVelocity * _speed;
        }

        _rigidBody.velocity += _walkVelocity;
    }

    private void FixedUpdate()
    {
        _velocity.SetXY(_rigidBody.velocity.x, _rigidBody.velocity.z);
    }

    private void LateUpdate()
    {
        //print("FLY: " + _flyVelocity.magnitude);
        //print("WALK: " + _walkVelocity.magnitude);
        //print(" ");

        if (_flyVelocity.magnitude > 0.02f)
        {
            _flyVelocity *= 0.99f;
            return;
        }
        else
        {
            _flyVelocity = Vector3.zero;
        }

        //_rigidBody.velocity += _flyVelocity;

        _walkVelocity *= 0;
    }

    private void reflect(Vector3 pNormal)
    {
        _normal.SetXY(pNormal.x, pNormal.z);

        _velocity = _velocity.Reflect(_normal, 1);
        Vector3 vector = new Vector3(_velocity.x, 0, _velocity.y);
        //_flyVelocity = vector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToUpper() == "WALL")
        {
            reflect(collision.contacts[0].normal);
        }
    }

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag.ToUpper() == "WEAPON")
        {
            Vector3 delta = transform.position - pOther.gameObject.transform.position;

            delta = delta.normalized * pOther.GetComponentInParent<Attack>().Force;
            delta.y = 0;
            _rigidBody.velocity += delta;
        }
    }
}
