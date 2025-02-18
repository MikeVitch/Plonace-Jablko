using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMenu_Manager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;

    public GameObject deathScreen;
    public GameObject endScreen;

    public void SetHealth(float health)
    {
        healthSlider.value = health;
        if (health <= 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0;
        }

        }

    public void SetMana(float mana)
    {
        manaSlider.value = mana;
       

    }

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        Time.timeScale = 0;
    }

}
