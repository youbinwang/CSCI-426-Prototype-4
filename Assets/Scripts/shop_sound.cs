using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop_sound : MonoBehaviour
{
    public AudioSource audiosource;

    void OnCollisionEnter2D(Collision2D collision)
    {
        audiosource.Play();
    }
}
