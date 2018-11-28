using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    [Header("Which player you are")]
    [Range(1, 4)]
    public int PLAYER = 1;

    [Space(3)]
    [Header("How much the % will go up")]
    [Range(0, 100)]
    public int ATTACK = 5;

    [Header("How fast you will walk")]
    [Range(0, 10)]
    public float SPEED = 5f;

    [Header("The % of how much slower you get tossed around")]
    [Range(0, 100)]
    public int DAMAGE_ABSORPTION;
}
