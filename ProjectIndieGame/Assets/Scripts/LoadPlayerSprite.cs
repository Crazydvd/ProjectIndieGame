using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerSprite : MonoBehaviour {

    [Range(1,4)]
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
        if(ControllerSettings.AmountOfPlayers() < _playerID) // remove HUD element if player not playing
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }

        populateCharacterList();

        int[,] listOfPlayers = { { (int)PlayerSettings.player1, PlayerSettings.player1Alt },
                                 { (int)PlayerSettings.player2, PlayerSettings.player2Alt },
                                 { (int)PlayerSettings.player3, PlayerSettings.player3Alt },
                                 { (int) PlayerSettings.player4, PlayerSettings.player4Alt }};

        int character = listOfPlayers[_playerID - 1, 0];
        int alt = listOfPlayers[_playerID - 1, 1];

        GetComponent<Image>().sprite = _characters[character, alt];
    }

    void populateCharacterList()
    {
        _characters = new Sprite[,] { { _char1Alt1, _char1Alt2, _char1Alt3, _char1Alt4 },
                                          { _char2Alt1, _char2Alt2, _char2Alt3, _char2Alt4 },
                                          { _char3Alt1, _char3Alt2, _char3Alt3, _char3Alt4 }};
    }

}
