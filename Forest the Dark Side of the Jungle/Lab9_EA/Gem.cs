using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the object has the Player tag
        {
            // Notify the GemCollector script
            FindObjectOfType<GemCollector>().CollectGem();

            // Destroy the gem object
            Destroy(gameObject);
        }
    }
}
