using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;
    private float xBound = 5;
    private float yBound = 4.5f;
    private float time;
    private float bestTime;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;
    public String bestPlayer;
    public String player;
    private Rigidbody playerRb;
    public float playerHealth = 100;
    public bool isAlive;

    private void Start()
    {
        bestPlayer = GameManager.Instance.bestPlayerName;
        player = GameManager.Instance.playerName;
        bestTime = GameManager.Instance.bestTime;
        isAlive = true;
        time = 0;
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ConstraintPlayerPosition();
    }
    private void Update()
    {
        if(isAlive)
        {
            IncreaseTime();
            if(time > bestTime)
            {
                bestTime = time;
                GameManager.Instance.bestTime = bestTime;
            }
        }
    }

    private void MovePlayer()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal"); 
        float verticalInput = Input.GetAxis("Vertical"); 

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.position += direction * speed * Time.deltaTime;
    }

    private void ConstraintPlayerPosition()
    {
        if(transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }

        if(transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if(transform.position.y < -yBound)
        {
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);
        }

        if(transform.position.y > yBound)
        {
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        }
    }

    private void IncreaseTime()
    {
        time += Time.deltaTime;
        timeText.text = "Time: " + MathF.Round(time);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with enemy.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
