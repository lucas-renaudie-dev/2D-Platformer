using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private float horizontalSpeed = 10;
    public Rigidbody2D myBody;
    private Animator anim;
    public bool grounded;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
                SoundManager.instance.PlaySound(jumpSound);
            }

            if (Input.GetKeyUp(KeyCode.Space) && myBody.linearVelocity.y > 0) {
                myBody.linearVelocity = new Vector2(myBody.linearVelocity.x, myBody.linearVelocity.y * 0.5f);
            }

            anim = GetComponent<Animator>();
            anim.SetBool("run", horizontalInput != 0);
    }

    private void Jump() {
        myBody.linearVelocity = Vector2.up * jumpHeight;
        grounded = false;
        anim.SetBool("Grounded", grounded);
        //SoundManager.instance.PlaySound(jumpSound);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            grounded = true;
            anim.SetBool("Grounded", grounded);
        }
    }
}
