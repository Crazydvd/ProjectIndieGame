using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHandler : MonoBehaviour
{

    public List<GameObject> players = new List<GameObject>();

    public List<GameObject> GetPlayers()
    {
        return players;
    }
}
