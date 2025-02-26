using UnityEngine;

public class NextRoomSign : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider;
    public bool signPassed = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !signPassed)
        {
            anim.SetTrigger("signPassed");
            collider.enabled = false;
            signPassed = true;
        }
    }
}
