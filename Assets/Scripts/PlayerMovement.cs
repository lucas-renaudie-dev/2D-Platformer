using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 10;
    public float horizontalSpeed = 10;
    public Rigidbody2D myBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (horizontalInput < 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            myBody.linearVelocity = Vector2.up * jumpHeight;
        }

        myBody.linearVelocity = new Vector2(horizontalInput * horizontalSpeed, myBody.linearVelocity.y);
    }
}
