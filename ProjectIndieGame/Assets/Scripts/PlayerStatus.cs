using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text damageUI;
    public Text livesUI;

    int damage = 0;
    int lives = 3;

    void Start()
    {
        damageUI.text = damageUI.text + damage + " %";
        livesUI.text = livesUI.text + lives;
    }

    public int GetDamage()
    {
        return damage;
    }

    public void IncreaseDamage(int pAmmount)
    {
        damage += pAmmount;
        damage = (damage > 999) ? 999 : damage;

        damageUI.text = "Damage : " + damage + " %";
    }

    public void ResetDamage()
    {
        damage = 0;
        damageUI.text = "Damage : " + damage + " %";
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
