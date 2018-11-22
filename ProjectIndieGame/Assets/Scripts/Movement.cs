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

    private Vector2 _velocity;
    private Vector2 _lateVelocity;
    private Vector2 _normal;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        forward = new Vector3(0, 0, _speed);
        backward = new Vector3(0, 0, -_speed);
        left = new Vector3(-_speed, 0, 0);
        right = new Vector3(_speed, 0, 0);

        _velocity = Vector2.zero;
        _lateVelocity = Vector2.zero;
        _normal = Vector2.zero;
    }

    void Update()
    {
        if (CANTMOVE)
        {
            Debug.Log(_rigidBody.velocity);
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
    }

    private void FixedUpdate()
    {
        //print("VELOCITY: " + _rigidBody.velocity);


        if (_rigidBody.velocity.magnitude > _speed)
        {
            _rigidBody.velocity += _walkVelocity;
        }
        else
        {
            _rigidBody.velocity = _walkVelocity;
        }
        _lateVelocity = _rigidBody.velocity;
        //_velocity.SetXY(_rigidBody.velocity.x, _rigidBody.velocity.z);
    }

    private void LateUpdate()
    {

        //_rigidBody.velocity += _flyVelocity;
    }

    private void reflect(Vector3 pNormal)
    {
        _normal.Set(pNormal.x, pNormal.z);
        _velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.z);
        _velocity = Vector2.Reflect(_lateVelocity, _normal);
        Vector3 vector = new Vector3(_velocity.x, 0, _velocity.y);
        _rigidBody.velocity = vector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToUpper() == "WALL")
        {
            //Debug.Log(_rigidBody.velocity + " - " + _rigidBody.velocity.magnitude + "mag" + _speed);
            //if (_rigidBody.velocity.magnitude > _speed)
            //{
                reflect(collision.contacts[0].normal);
            //}
        }
    }

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.gameObject.tag.ToUpper() == "WEAPON")
        {
            Vector3 delta = transform.position - pOther.gameObject.transform.position;

            delta = delta.normalized * pOther.GetComponentInParent<Attack>().Force;
            delta.y = 0;
            //Debug.Log("DAMAGE:" + delta);
            _rigidBody.velocity = delta;
        }
    }
}
