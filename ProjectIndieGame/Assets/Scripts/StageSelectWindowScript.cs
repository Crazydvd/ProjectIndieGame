using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectWindowScript : MonoBehaviour {

    [SerializeField] GameObject _characterSelector;
    private void onEnable()
    {
        PlayerSettings.stageSelected = -1;
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Decline_P" + ControllerSettings.player1Joystick) && PlayerSettings.stageSelected == -1)
        {
            _characterSelector.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
