using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    Rigidbody _rigidbody;
    Vector3 startPosition;
    Vector3 zero;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        zero = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.ToUpper() == "BOUNDARY")
        {
            Debug.Log("boop");
            _rigidbody.velocity = zero;
            this.transform.position = startPosition;
        }
    }
}
