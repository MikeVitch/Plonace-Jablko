using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int level;
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public int currentExp;
    public int maxExp;
    [Space]
    public int strength;
    public int intelligence;
    public int endurance;
    public int agility;
    public int luck;
    [Space]
    public Slider healthBar;
    public Slider manaBar;
    public Slider expBar;
    [Space]
    public TextMeshProUGUI healthSliderDispaly;
    public TextMeshProUGUI manaSliderDisplay;

    private void Update()
    {
        ChangeSliderUI();

        if(currentExp >= maxExp)
        {
            level += 1;
            //currentStatPoints += 5;
            currentExp = 0;
        }
    }
    public void ChangeSliderUI()
    {
        healthBar.value = currentHealth;
        manaBar.value = currentMana;
        expBar.value = currentExp;

        healthBar.maxValue = maxHealth;
        manaBar.maxValue = maxMana;
        expBar.maxValue = maxExp;

        healthSliderDispaly.text = currentHealth + " / " + maxHealth;
        manaSliderDisplay.text = currentMana + " / " + maxMana;
    }
}
