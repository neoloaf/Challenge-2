using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text scoreText;
    public Text livesText;
    private int scoreValue;
    private int livesValue;
    public Text winLossText;

    public AudioClip bgMusic;
    public AudioClip victory;
    public AudioSource musicSource;

    Animator anim;
    private bool facingRight = true;
    private bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = "";
        livesText.text = "";
        winLossText.text = "";
        scoreValue = 0;
        livesValue = 3;
        SetScoreText();
        SetLivesText();

        musicSource.clip = bgMusic;
        musicSource.Play();
        musicSource.loop = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        //if the horizontalmovement is less than or above 0 (moving left or right)
        if(hozMovement > 0 || hozMovement < 0)
        {
            anim.SetInteger("State", 1);
        }
        
        //otherwise the movement is 0 and the state is idle
        else if(hozMovement == 0)
        {
            anim.SetInteger("State", 0);
        }

        //flipper!
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        /*
        if(Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isJumping", true);
        }
        */
        if (onGround == true)
        {
            anim.SetBool("isJumping", false);
        }
        //escape function
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    //2d trigger prevents player from losing momentum due to hitting coin
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            scoreValue += 1;
            SetScoreText();
        }

        if(scoreValue == 4)
        {
            rd2d.velocity = new Vector2(0.0f, 0.0f);
            transform.position = new Vector2(70f, 0);
            livesValue = 3;
            SetLivesText();
        }
    }
    
    //2d collision stops player momentum for hitting enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            collision.gameObject.SetActive(false);
            SetLivesText();
        }

        if (livesValue == 0)
        {
            //stops player from being able to control the player
            Destroy(this);
            //stops the player from moving right there
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            onGround = true;
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
            }
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
        
        if (scoreValue == 8)
        {
            winLossText.text = "You win! :D Game created by Julianne Truong";

            musicSource.clip = victory;
            musicSource.Play();
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + livesValue.ToString();
        
        if (livesValue == 0)
        {
            winLossText.text = "You Lost! :(";
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}
