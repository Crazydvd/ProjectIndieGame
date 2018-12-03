using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Text damageUI;
    public Image[] _livesUI_full;

    private BackgroundMusic _bgMusic;

    int damage = 0;
    int lives = 3;

    void Start()
    {
        damageUI.text = damageUI.text + damage + "%";
        _bgMusic = Camera.main.GetComponent<BackgroundMusic>();
    }

    public int GetDamage()
    {
        return damage;
    }

    public void IncreaseDamage(int pAmmount)
    {
        damage += pAmmount;
        damage = (damage > 999) ? 999 : damage;

        damageUI.text = damage + "%";
    }

    public void ResetDamage()
    {
        damage = 0;
        damageUI.text = damage + "%";
    }

    public int GetLives()
    {
        return lives;
    } 

    public void DecreaseLives()
    {
        lives--;
        for(int i = 0; i < _livesUI_full.Length; i++)
        {
            if (i == 3 - lives)
            {
                _livesUI_full[i].gameObject.SetActive(true);
            }
            else
            {
                _livesUI_full[i].gameObject.SetActive(false);
            }
        }
        _bgMusic.SetLifesParameter(4 - lives);
    }
}
