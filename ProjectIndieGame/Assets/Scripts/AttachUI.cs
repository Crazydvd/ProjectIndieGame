using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachUI : MonoBehaviour
{
    [SerializeField] GameObject _name;

    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        _name.transform.position = namePos;
    }
}
