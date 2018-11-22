using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text livesUI;

    int damage = 0;
    int lives = 3;

    void Start()
    {
        livesUI.text = livesUI.text + lives;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void IncreaseDamage(int pAmmount)
    {
        damage += pAmmount;
    }

    public int GetLives()
    {
        return lives;
    } 

    public void DecreaseLives()
    {
        lives--;
        livesUI.text = livesUI.text.Remove(livesUI.text.Length - 1) + lives;
    }
}
