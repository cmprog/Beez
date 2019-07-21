using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private FollowMovement mMovement;

    public GameObject bugZapper;

    public float timeRemaining;

    private void Start()
    {
        this.mMovement = this.GetComponent<FollowMovement>();
    }

    private void FixedUpdate()
    {
        this.timeRemaining -= Time.deltaTime;
        if (this.timeRemaining < 0)
        {
            this.mMovement.target = this.bugZapper;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameObjectTags.Player))
        {
            var lPollenCollectable = collision.GetComponent<PollenCollectable>();
            lPollenCollectable.CurrentPollen *= 0.5;

            Destroy(this.gameObject);
        }
    }
}
