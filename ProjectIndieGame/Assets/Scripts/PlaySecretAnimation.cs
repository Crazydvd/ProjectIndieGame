using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySecretAnimation : MonoBehaviour
{

    [SerializeField] GameObject _sword;
    [SerializeField] Image _white;

    bool turnWhite = false;

    private void Start()
    {
        GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    // Update is called once per frame
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.F12))
        {
            GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            animator.enabled = true;
            Invoke("ActivateSword", 4.5f);
        }

        if (turnWhite)
        {
            Color newcolor = new Color(_white.color.r, _white.color.g, _white.color.b, _white.color.a + 0.004f);
            _white.color = newcolor;
        }
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
