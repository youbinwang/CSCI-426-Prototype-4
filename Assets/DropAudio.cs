using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAudio : MonoBehaviour
{
    public AudioSource audioSource;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            audioSource.Play();
        }
    }
}
