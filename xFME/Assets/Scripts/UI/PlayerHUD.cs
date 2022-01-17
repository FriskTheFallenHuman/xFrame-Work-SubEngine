using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText = default;
    [SerializeField] private TextMeshProUGUI staminaText = default;

   // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        PlayerController.OnDamage += UpdateHealth;
        PlayerController.OnHeal += UpdateHealth;
        PlayerController.OnStaminaChange += UpdateStamina;
    }

    // This function is called when the behaviour becomes disabled.
    private void OnDisable()
    {
        PlayerController.OnDamage -= UpdateHealth;
        PlayerController.OnHeal -= UpdateHealth;
        PlayerController.OnStaminaChange -= UpdateStamina;
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    private void Start()
    {
        /* Set out health and stamina to display at the start */
        UpdateHealth( 100 );
        UpdateStamina( 100 );
    }

    /* Keep track of our health */
    private void UpdateHealth( float currentHealth )
    {
        healthText.text = "+ " + currentHealth.ToString( "00" );
    }

    /* Keep track of our stamina */
    private void UpdateStamina( float currentStamina )
    {
        staminaText.text = "S " + currentStamina.ToString( "00" );
    }
}
