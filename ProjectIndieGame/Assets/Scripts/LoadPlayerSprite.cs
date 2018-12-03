using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerSprite : MonoBehaviour {

    [Range(1,2)]
    [SerializeField] int _playerID = 1;

    [SerializeField] Sprite _char1Alt1;
    [SerializeField] Sprite _char1Alt2;
    [SerializeField] Sprite _char1Alt3;
    [SerializeField] Sprite _char1Alt4;

    [SerializeField] Sprite _char2Alt1;
    [SerializeField] Sprite _char2Alt2;
    [SerializeField] Sprite _char2Alt3;
    [SerializeField] Sprite _char2Alt4;

    [SerializeField] Sprite _char3Alt1;
    [SerializeField] Sprite _char3Alt2;
    [SerializeField] Sprite _char3Alt3;
    [SerializeField] Sprite _char3Alt4;

    Sprite[,] _characters;

    void Start()
    {
        populateCharacterList();

        int character = PlayerPrefs.GetInt("Char_P" + _playerID);
        int alt = PlayerPrefs.GetInt("Char_color_P" + _playerID);

        GetComponent<Image>().sprite = _characters[character, alt];
    }

    void populateCharacterList()
    {
        _characters = new Sprite[,] { { _char1Alt1, _char1Alt2, _char1Alt3, _char1Alt4 },
                                          { _char2Alt1, _char2Alt2, _char2Alt3, _char2Alt4 },
                                          { _char3Alt1, _char3Alt2, _char3Alt3, _char3Alt4 }};
    }

}
