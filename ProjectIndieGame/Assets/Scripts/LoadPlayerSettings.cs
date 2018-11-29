using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerSettings : MonoBehaviour {

    [Range(1,2)]
    [SerializeField] int _playerID = 1;
    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;
    [SerializeField] GameObject _character3;

    private PlayerParameters _parameters;

    GameObject _usedModel;

    // Use this for initialization
    void Start () {

        _parameters = transform.root.GetComponent<PlayerParameters>();
        int ID = PlayerPrefs.GetInt("Char_P" + _parameters.PLAYER);
        switch (ID)
        {
            case 0:
                _usedModel = _character1;
                break;
            case 1:
                _usedModel = _character2;
                break;
            case 2:
                _usedModel = _character3;
                break;
            default:
                _usedModel = _character1;
                break;

        }

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        meshRenderer.material = _usedModel.GetComponent<MeshRenderer>().sharedMaterial;
        meshFilter.mesh = _usedModel.GetComponent<MeshFilter>().sharedMesh;
	}
}
