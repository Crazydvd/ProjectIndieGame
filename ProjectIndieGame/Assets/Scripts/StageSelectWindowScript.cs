using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _characterSelector;
    private void Start()
    {
        PlayerPrefs.SetInt("Stage", -1);
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Decline_P1") && PlayerPrefs.GetInt("Stage") == -1)
        {
            _characterSelector.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
