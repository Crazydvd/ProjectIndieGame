using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToPosition : MonoBehaviour
{
    public bool ownPosition = true;

    private Vector3 _oldPosition;

    private void Start()
    {
        SetOriginalPosition();
    }

    public void GoToPos()
    {
        transform.position = _oldPosition;
    }

    public void SetOriginalPosition()
    {
        _oldPosition = transform.position;
    }
}
