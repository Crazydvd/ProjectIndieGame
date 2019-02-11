using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerSettings : MonoBehaviour
{

    [SerializeField] GameObject _character1;
    [SerializeField] Material _char1Alt1;
    [SerializeField] Material _char1Alt2;
    [SerializeField] Material _char1Alt3;
    [SerializeField] GameObject _character2;
    [SerializeField] Material _char2Alt1;
    [SerializeField] Material _char2Alt2;
    [SerializeField] Material _char2Alt3;
    [SerializeField] GameObject _character3;
    [SerializeField] Material _char3Alt1;
    [SerializeField] Material _char3Alt2;
    [SerializeField] Material _char3Alt3;

    private PlayerParameters _parameters;
    Material[,] _characters;

    GameObject _usedModel;

    // Use this for initialization
    void Start()
    {
        _parameters = transform.root.GetComponent<PlayerParameters>();
        Debug.Log(ControllerSettings.AmountOfPlayers());

        if (ControllerSettings.AmountOfPlayers() < _parameters.PLAYER)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }

        populateCharacterList();
        int[,] listOfPlayers = PlayerSettings.ListOfAllPlayers();

        int ID = listOfPlayers[_parameters.PLAYER - 1, 0];
        int altID = listOfPlayers[_parameters.PLAYER - 1, 1];
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

        if (altID == 0)
        {
            meshRenderer.material = _usedModel.GetComponent<MeshRenderer>().sharedMaterial;
        }
        else
        {
            meshRenderer.material = _characters[ID, altID - 1];
        }
        Material = meshRenderer.material.name;

        meshFilter.mesh = _usedModel.GetComponent<MeshFilter>().sharedMesh;

        loadStats(ID);
    }

    void populateCharacterList()
    {
        _characters = new Material[,] { { _char1Alt1, _char1Alt2, _char1Alt3},
                                          { _char2Alt1, _char2Alt2, _char2Alt3 },
                                          { _char3Alt1, _char3Alt2, _char3Alt3 }};
    }

    public string Material { get; set; }

    void loadStats(int ID)
    { 
        PlayerParameters parameters = transform.root.GetComponent<PlayerParameters>();
        if (ID == 0) // RAM
        {
            parameters.ATTACK = 4;
            parameters.SPEED = 7;
            parameters.DAMAGE_ABSORPTION = 10;
        }
        else if (ID == 1) // BULL
        {
            parameters.ATTACK = 6;
            parameters.SPEED = 3.5f;
            parameters.DAMAGE_ABSORPTION = 0;
        }
        else if (ID == 2) // PIG
        {
            parameters.ATTACK = 5;
            parameters.SPEED = 5;
            parameters.DAMAGE_ABSORPTION = 20;
        }
    }
}
