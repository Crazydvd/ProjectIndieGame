using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToPosition : MonoBehaviour
{
    public Transform Transform;
    public bool ownPosition = true;

    private Vector3 _oldPosition;

    private void Start()
    {
        SetOriginalPosition();
    }

    public void GoToPos()
    {
        if (ownPosition)
        {
            transform.position = _oldPosition;
            return;
        }

        transform.position = Transform.position;
    }

    public void SetOriginalPosition()
    {
        _oldPosition = transform.position;
    }
}
