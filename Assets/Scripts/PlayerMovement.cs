using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 50f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    public AudioSource audioData;
    
    //Speed Up when delivered a box
    public float speedIncrease = 10f;
    
    public TextMeshProUGUI speedText;

    public GameObject gameEndUI;
    public ParticleSystem playerDied;
    public GameObject circle;
    private TrailRenderer playerTrail;

    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameEndUI.SetActive(false);
        playerTrail = GetComponent<TrailRenderer>();

        rb.drag = 3f;
    }

    void Update()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
                
        speedText.text = (moveSpeed / 50f).ToString("0.0");
    }

    void FixedUpdate()
    {
        if (movementInput.magnitude > 0)
        {
            rb.AddForce(movementInput.normalized * moveSpeed);
        }
    }

    public void IncreaseSpeed()
    {
        moveSpeed += speedIncrease;
    }
    
    public IEnumerator FlashSprite()
    {
        SpriteRenderer circleSpriteRenderer = circle.GetComponent<SpriteRenderer>();
        Color originalColor = circleSpriteRenderer.color;
        Color flashColor = Color.white;
        
        for (int i = 0; i < 3; i++)
        {
            circleSpriteRenderer.color = flashColor;
            yield return new WaitForSeconds(0.1f);
            circleSpriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            audioData.Play();
            GameEnd();
        }
    }

   public void GameEnd()
    {
        //When Player hit by a Car
        Debug.Log("GameEnd!");
        gameManager.StopTimer();
        
        playerTrail.Clear();
        playerTrail.enabled = false;
        
        circle.SetActive(false);
        playerDied.Play();
        
        StartCoroutine(DestroyPlayer());
    }
   
    IEnumerator DestroyPlayer()
        { 
            yield return new WaitForSeconds(1f); 
            Destroy(gameObject);
           gameEndUI.SetActive(true);
        }
}
