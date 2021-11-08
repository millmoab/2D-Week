using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody2D;

    public float runSpeed= 5;
    public float jumpSpeed= 200f;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public TextMeshProUGUI itemText;
    private int item;






    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        item= 0;
        SetScoreText ();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            int levelMask= LayerMask.GetMask("Level");
            if(Physics2D.BoxCast(transform.position, new Vector2(1,.1f),0f, Vector2.down,.01f,levelMask) )
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction= new Vector2(horizontalInput * runSpeed * Time.deltaTime, 0);
        rigidBody2D.AddForce(new Vector2(horizontalInput, 0));

        if (rigidBody2D.velocity.x > 0)
        {
            spriteRenderer.flipX =false;
        }
        else
        {
            spriteRenderer.flipX=true;
        }

        if(Mathf.Abs(horizontalInput) > 0f)
        {
            animator.SetBool("isRunning", true);
        }

        else
        {
            animator.SetBool("isRunning", false);
        }

        
    }
    void OnTriggerEnter2D (Collider2D other)
    {
    if (other.gameObject.CompareTag("item"))
        {
            other.gameObject.SetActive (false);
            item= item +1;
            SetScoreText ();
        }
    }
    void Jump()
    {
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpSpeed);
        
    }

    void SetScoreText ()
    {
        itemText.text= "Gem Count:" + item.ToString();
    }

}
