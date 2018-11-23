﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private Vector3 _offset;

    public bool SetToPosition = false;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - _gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameObject == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (SetToPosition)
        {
            transform.position = _gameObject.transform.position;
            return;
        }

        transform.position = _gameObject.transform.position + _offset;
    }
}
