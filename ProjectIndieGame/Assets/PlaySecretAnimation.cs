using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySecretAnimation : MonoBehaviour {

    [SerializeField] GameObject _sword;
    [SerializeField] Image _white;

    bool turnWhite = false;
	
	// Update is called once per frame
	void Update () {
        Animator animator = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.F12))
        {
            
            animator.enabled = true;
            Invoke("ActivateSword", 4.5f);

        }

        if (turnWhite)
        {
            Color newcolor = new Color(_white.color.r, _white.color.g, _white.color.b, _white.color.a + 0.0025f);
            _white.color = newcolor;
        }
        Color color = new Color(_white.color.r, _white.color.g, _white.color.b, _white.color.a);
    }

    void ActivateSword()
    {
        _sword.SetActive(true);
        Invoke("ActivateWhite", 2f);
        _sword.GetComponent<Animator>().enabled = true;
    }

    void ActivateWhite()
    {
        turnWhite = true;
    }
}
