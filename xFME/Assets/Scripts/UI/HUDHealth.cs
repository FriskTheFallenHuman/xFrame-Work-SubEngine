/*
                             MIT License

                    Copyright (c) 2023 Krispy

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/
using UnityEngine;
using TMPro;

public class HUDHealth : MonoBehaviour
{
    /* Variables */
    [SerializeField] private TextMeshProUGUI healthText = default;

    /* This function is called when the object becomes enabled and active. */
    private void OnEnable()
    {
        PlayerController.OnDamage += UpdateHealth;
        PlayerController.OnHeal += UpdateHealth;
    }

    // This function is called when the behaviour becomes disabled.
    private void OnDisable()
    {
        PlayerController.OnDamage -= UpdateHealth;
        PlayerController.OnHeal -= UpdateHealth;
    }

    /* Awake is called when the script instance is being loaded. */
    public void Awake()
    {
        /* Get our healthText */
        if ( healthText == null )
            healthText = GetComponent<TextMeshProUGUI>();

        /* Set out health at the start */
        UpdateHealth( 100 );
    }

    /* Keep track of our health */
    private void UpdateHealth( float currentHealth )
    {
        healthText.text = "+ " + currentHealth.ToString("00");
    }
}
