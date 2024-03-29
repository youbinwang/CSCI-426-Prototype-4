using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private GameObject player;
    private static bool haveBox = false;
    
    public AudioClip boxPickSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = boxPickSound;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !haveBox)
        {
            player = other.gameObject;
            boxPickup();
        }
        
        if (other.gameObject.CompareTag("Shop"))
        {
            if (haveBox)
            {
                boxDrop();
            }

            if (!haveBox)
            {
                Debug.Log("You don't have a Package!");
                return;
            }
        }
    }

    void boxPickup()
    {
        haveBox = true;
        audioSource.Play();
        //More Functions: Play Audio, Visual Effect, etc.
    }

    void boxDrop()
    {
        Debug.Log("You get 1 Point!");
        
        GameManager.instance.UpdateScore(1);
        player.GetComponent<PlayerMovement>().IncreaseSpeed();
        
        player.GetComponent<PlayerMovement>().StartCoroutine(player.GetComponent<PlayerMovement>().FlashSprite());
        
        Destroy(gameObject);
        haveBox = false;
        //More Functions: Play Audio, Visual Effect, etc.
    }
    
    void Update()
    {
        if (haveBox && player != null)
        {
            Vector3 playerCenter = player.transform.position;
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) return;

            Vector3 alignmentOffset = new Vector3(spriteRenderer.bounds.extents.x, -spriteRenderer.bounds.extents.y, 0);
            transform.position = playerCenter - alignmentOffset;
        }
    }
    
    public static void ResetBoxState()
    {
        haveBox = false;
    }
}
