using UnityEngine;

public class NextRoomScript : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider;
    private bool signPassed = false;
    [SerializeField] private Transform checkpoint; //TODO: set the new checkpoint to be the next room
    [SerializeField] private LogicManager logic;

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

            logic.setRespawnPoint(checkpoint);
        }
    }
}
