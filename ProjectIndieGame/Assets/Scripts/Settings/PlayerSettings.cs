using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public enum character
    {
        GOAT,
        BULL,
        PIG
    }

    public static bool[] playerHasLeft = new bool[] { false, false, false, false };
    public static bool[] playersSelected = new bool[] { false, false, false, false };

    public static character player1 = character.GOAT;
    public static character player2 = character.GOAT;
    public static character player3 = character.GOAT;
    public static character player4 = character.GOAT;

    public static int player1Alt = -1;
    public static int player2Alt = -1;
    public static int player3Alt = -1;
    public static int player4Alt = -1;

    public static int stageSelected = -1;

    public static int[,] ListOfAllPlayers() {
        return new int[,]  { { (int)PlayerSettings.player1, PlayerSettings.player1Alt },
                                 { (int)PlayerSettings.player2, PlayerSettings.player2Alt },
                                 { (int)PlayerSettings.player3, PlayerSettings.player3Alt },
                                 { (int)PlayerSettings.player4, PlayerSettings.player4Alt } };
    }

    public static bool[,] takenChararacters = new bool[,]{{false, false, false, false},
                                              {false, false, false, false},
                                              {false, false, false, false},
                                              {false, false, false, false}};

    public static void ResetTakenCharacters()
    {
        takenChararacters = new bool[,]{{false, false, false, false},
                                              {false, false, false, false},
                                              {false, false, false, false},
                                              {false, false, false, false}};

        playersSelected = new bool[] { false, false, false, false };

        player1 = character.GOAT;
        player2 = character.GOAT;
        player3 = character.GOAT;
        player4 = character.GOAT;

        player1Alt = 0;
        player2Alt = -1;
        player3Alt = -1;
        player4Alt = -1;
    }
}
