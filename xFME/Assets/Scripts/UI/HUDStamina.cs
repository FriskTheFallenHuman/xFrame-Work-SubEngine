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

public class HUDStamina : MonoBehaviour
{
    /* Variables */
    [SerializeField] private TextMeshProUGUI staminaText = default;

   // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        PlayerController.OnStaminaChange += UpdateStamina;
    }

    // This function is called when the behaviour becomes disabled.
    private void OnDisable()
    {
        PlayerController.OnStaminaChange -= UpdateStamina;
    }

    /* Awake is called when the script instance is being loaded. */
    public void Awake()
    {
        /* Get our staminaText */
        if ( staminaText == null )
            staminaText = GetComponent<TextMeshProUGUI>();

        /* Set out stamina at the start */
        UpdateStamina( 100 );
    }

    /* Keep track of our stamina */
    private void UpdateStamina( float currentStamina )
    {
        staminaText.text = "S " + currentStamina.ToString( "00" );
    }
}
