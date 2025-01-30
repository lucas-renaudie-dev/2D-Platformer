using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private Animator anim;
    private Collider2D collider;
    private bool checkpointPassed = false;
    [SerializeField] private Transform checkpoint; //TODO: set the new checkpoint to be the next room
    [SerializeField] private LogicManager logic;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !checkpointPassed)
        {
            anim.SetTrigger("checkpointPassed");
            collider.enabled = false;
            checkpointPassed = true;

            logic.setRespawnPoint(checkpoint);
        }
    }
}
