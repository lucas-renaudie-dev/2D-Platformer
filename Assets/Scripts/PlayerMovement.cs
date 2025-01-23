using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isAlive = true;
    public LogicManager logic;
    public float jumpHeight = 10;
    public float horizontalSpeed = 10;
    public Rigidbody2D myBody;
    private Animator anim;
    private bool grounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            float horizontalInput = Input.GetAxis("Horizontal");
            myBody.linearVelocity = new Vector2(horizontalInput * horizontalSpeed, myBody.linearVelocity.y);

            if (horizontalInput > 0) {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (horizontalInput < 0) {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && grounded) {
                Jump();
            }

            anim = GetComponent<Animator>();
            anim.SetBool("run", horizontalInput != 0);
            anim.SetBool("Grounded", grounded);
            anim.SetBool("dead", false);
        }

        else {
            anim.SetBool("dead", true);
        }
    }

    private void Jump() {
        myBody.linearVelocity = Vector2.up * jumpHeight;
        anim.SetTrigger("Jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
        }
    }
}
