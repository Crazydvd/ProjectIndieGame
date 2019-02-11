using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSettings : MonoBehaviour
{
    /** Setting the player ID to a joystick
    * -1 = no controller set yet
    * 0 = keyboard + mouse
    */
    public static int player1Joystick = -1;
    public static int player2Joystick = -1;
    public static int player3Joystick = -1;
    public static int player4Joystick = -1;

    public static void ResetPlayers(bool player1 = true){
        if (player1)
        {
            player1Joystick = -1;
        }
        player2Joystick = -1;
        player3Joystick = -1;
        player4Joystick = -1;
    }

    public static int[] listOfPlayers()
    {
        return new int[] { player1Joystick, player2Joystick, player3Joystick, player4Joystick };
    }

    public static int AmountOfPlayers()
    {
        int[] listOfPlayers = { player1Joystick, player2Joystick, player3Joystick, player4Joystick };
        int amount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (listOfPlayers[i] != -1)
            {
                amount++;
            }
        }
        return amount;
    }
}
