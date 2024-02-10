using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private GameObject player;
    private static bool haveBox = false;
    private PlayerMovement _playerMovement;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !haveBox)
        {
            player = other.gameObject;
            boxPickup();
        }
        
        if (other.gameObject.CompareTag("Car"))
        {
            _playerMovement.gameFail();
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
        //More Functions: Play Audio, Visual Effect, etc.
    }

    void boxDrop()
    {
        Debug.Log("You get 1 Point!");
        GameObject.Destroy(gameObject);
        haveBox = false;
        //Get a Point
        //Movement Speed +
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
    
    
}
