using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    private int score;
    private int scoreValue = 0;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private int livesValue = 3;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = scoreValue.ToString();
        livesText.text = "Lives: " + livesValue.ToString();
        livesValue = 3;

        SetScoreText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 4)
         {
            winTextObject.SetActive(true);
            Destroy(gameObject);

            // Calls sound script and plays win sound
            WinSoundScript.PlaySound("WinSound");
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + livesValue.ToString();
    if (livesValue <= 0)
        {
            loseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    
    
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            scoreText.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetLivesText();
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}