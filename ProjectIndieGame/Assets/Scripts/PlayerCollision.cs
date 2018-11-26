using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToUpper() == "PLAYER")
        {
            Rigidbody rigidbody2 = collision.gameObject.GetComponent<Rigidbody>();
            if (rigidbody2.velocity.magnitude > _rigidbody.velocity.magnitude)
            {
                _rigidbody.velocity = rigidbody2.velocity * 0.5f;
            }
        }
    }
}
