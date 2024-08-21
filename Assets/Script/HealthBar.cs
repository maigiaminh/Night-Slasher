using System.Data;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text HP_Text;
    public Slider slider;
    public Gradient gradient;
    public float health; 
    public Image fill;
    public float currentHealth;
    private float MaxHp;
    public PlayerController playerController;
    public GameObject Boss;

    public void UpdateHealth(float health, float maxHealth)
    {
        if(Boss == null){
            HP_Text.text = health.ToString() + " / " + maxHealth.ToString();      
        }
        else{
            HP_Text.text = (health * 100 / maxHealth).ToString() + "%";
        }
    }

    // public void UpdateBar(float value, float maxValue, string text)
    // {
    //     HP_Text.text = text;
    // }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public float SetHealth(float health){
        slider.value = health;      
        fill.color = gradient.Evaluate(slider.normalizedValue);
        currentHealth = health; 
        return currentHealth;
    }   

     void Start()
    {          
        
        if(playerController == null){
            MaxHp = health;           
        }
        else{
            MaxHp = playerController.HP;
        }   

        SetMaxHealth(MaxHp);  
        currentHealth = SetHealth(MaxHp);
        UpdateHealth(MaxHp, MaxHp);
    }
    void Update()
    {
        currentHealth = SetHealth(currentHealth);
    }
    // IEnumerator TestHealthDecrease()
    // {
    //      currentHealth = health;

    //     for (int i = health; i >= 0; i -= 5)
    //     {
    //         currentHealth = i;
    //         UpdateHealth(currentHealth, health);

    //         SetHealth(currentHealth);
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }
}
