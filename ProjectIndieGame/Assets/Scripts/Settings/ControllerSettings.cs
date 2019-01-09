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

    public static void ResetPlayers(){
        player1Joystick = -1;
        player2Joystick = -1;
        player3Joystick = -1;
        player4Joystick = -1;
    }
}
