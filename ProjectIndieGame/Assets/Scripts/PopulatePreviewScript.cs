using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulatePreviewScript : MonoBehaviour {

    [SerializeField] GameObject _char1Alt1;
    [SerializeField] GameObject _char1Alt2;
    [SerializeField] GameObject _char1Alt3;
    [SerializeField] GameObject _char1Alt4;

    [SerializeField] GameObject _char2Alt1;
    [SerializeField] GameObject _char2Alt2;
    [SerializeField] GameObject _char2Alt3;
    [SerializeField] GameObject _char2Alt4;

    [SerializeField] GameObject _char3Alt1;
    [SerializeField] GameObject _char3Alt2;
    [SerializeField] GameObject _char3Alt3;
    [SerializeField] GameObject _char3Alt4;

    GameObject[,] _characters;

    // Use this for initialization
    void Start () {
        populateCharacterList();
	}

    void populateCharacterList()
    {
        _characters = new GameObject[,] { { _char1Alt1, _char1Alt2, _char1Alt3, _char1Alt4 },
                                          { _char2Alt1, _char2Alt2, _char2Alt3, _char2Alt4 },
                                          { _char3Alt1, _char3Alt2, _char3Alt3, _char3Alt4 }};
    }

    public void SetModel(int character, int color)
    {
        if(_characters == null)
        {
            populateCharacterList();
        }

        for (int i = transform.childCount; i > 0; i--)
        {
            Destroy(transform.GetChild(i - 1).gameObject);
        }

        Instantiate(_characters[character, color], transform);
    }
}
