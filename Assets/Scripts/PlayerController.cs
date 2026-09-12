using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private float speed = 10.0f;
    private float xBound = 5;
    private float yBound = 4.5f;
    private float time;
    private float bestTime;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI playerHealthText;
    public String bestPlayer;
    public String player;
    private Rigidbody playerRb;
    public float playerHealth = 100;
    public TextMeshProUGUI gameOverText;
    public Button menuButton;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    private void Start()
    {
        bestPlayer = GameManager.Instance.bestPlayerName;
        player = GameManager.Instance.playerName;
        bestTime = GameManager.Instance.bestTime;
        
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
        ShowBestScore();
        if(GameManager.Instance.isGameActive)
        {
            IncreaseTime();
            if(time > bestTime)
            {
                bestTime = time;
                bestPlayer = player;
                GameManager.Instance.bestPlayerName = bestPlayer;
                GameManager.Instance.bestTime = bestTime;
            }
        }
        GameManager.Instance.SaveData();
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

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealthText.text = "Health : " + playerHealth;
        if(playerHealth<=0)
        {
            GameOver();
        }
    }

    public void ShowBestScore()
    {
        bestTimeText.text = "Best Time :" + bestPlayer + " : " + MathF.Round(bestTime);
    }

    public void GameOver()
    {
        menuButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        GameManager.Instance.isGameActive = false;
    }

    public void Gotomenu()
    {
        GameManager.Instance.LoadData();
        SceneManager.LoadScene(0);
    }
}
