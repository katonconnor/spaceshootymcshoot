using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldup : MonoBehaviour
{
    // When another object enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touches the upgrade
        if (collision.CompareTag("Player"))
        {
            // Get the player's script
            player playerScript = collision.GetComponent<player>();

            if (playerScript != null)
            {
                // Increase the player's shield count
                playerScript.shield++;

                // Destroy the upgrade object
                Destroy(gameObject);
            }
        }
    }
}