using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Transform target;
    private float speed;
    public float maxSpeed = 20f;
    private GameObject player;
    private PlayerMovement _playerMovement;
    public LayerMask carLayer;
    public float detectionDistance = 3.0f; 

    void Start()
    {
        speed = Random.Range(6f, 20f);
        player = GameObject.FindWithTag("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        AdjustSpeed();
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

       
        if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
           
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (_playerMovement != null)
            {
                _playerMovement.GameEnd();
            }
            else
            {
                Debug.LogError("PlayerMovement component is not found!");
            }
        }
    }
    
    void AdjustSpeed()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, detectionDistance, carLayer);
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
           
            CarMovement carInFront = hit.collider.gameObject.GetComponent<CarMovement>();
            if (carInFront != null)
            {
                speed = Mathf.Min(speed, carInFront.speed);
            }
        }
        else
        {
            speed = Random.Range(6f, maxSpeed);
        }
    }
}
