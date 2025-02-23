using System;
using UnityEngine;

public class AtesDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Restart Trigger")
        {
            Destroy(gameObject);
        }
    }
}
