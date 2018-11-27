using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerSettings : MonoBehaviour {

    [Range(1,2)]
    [SerializeField] int _playerID = 1;
    [SerializeField] GameObject _character1;
    [SerializeField] GameObject _character2;
    [SerializeField] GameObject _character3;

    GameObject _usedModel;

    // Use this for initialization
    void Start () {
        int ID = PlayerPrefs.GetInt("Char_P" + _playerID);
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
