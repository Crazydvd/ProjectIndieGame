using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float destroyTime = 3;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
