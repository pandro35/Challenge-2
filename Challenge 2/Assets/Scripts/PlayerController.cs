using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    private bool facingRight = true;

    Animator anim;

    private int count;
    private int lives;
    public Text winText;
    public Text livesText;
    public Text loseText;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetWinText();
        SetLivesText();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        //Script to flip sprite
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            if (lives > 0)
            {
                count = count + 1;
            }
            SetWinText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            if (count < 20 && lives != 0)
            {
                lives = lives - 1;
            }
            SetLivesText();
        }
    }

    void SetWinText()
    {
        if (count == 8)
        {
            winText.text = "You win! Game Created by Alejandro Porras";

        }
        else if (count == 4)
        {
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();
            transform.position = new Vector2(x: 66.33f, y: -1.46f);
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            anim.SetInteger("State", 4);
            loseText.text = "You Lose! Game Created by Alejandro Porras";
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("State", 2);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("State", 2);
            }

            else if (Input.GetKey(KeyCode.W))
            {
                anim.SetInteger("State", 3);
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }

            else
            {
                anim.SetInteger("State", 1);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale; Scaler.x = Scaler.x * -1; transform.localScale = Scaler;
    }
}


