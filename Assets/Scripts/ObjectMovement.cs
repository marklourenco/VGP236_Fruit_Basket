using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Rigidbody2D rb = null;

    private Vector3 desiredSpeed = Vector3.zero;

    private float speed = 0.0f;

    private float randomRotation = 0.0f;

    private float randomMove = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        randomRotation = Random.Range(-6, 6);
        randomMove = Random.Range((float)-0.025, (float)0.025);

        if (SpawnManager.Instance != null)
        {
            speed = SpawnManager.Instance.GetSpeed();
        }
        else
        {
            Debug.LogError("SpawnManager instance is not initialized.");
        }

        //while()


        //if (GameManager.Instance.GetTimer() > 5.0f)
        //{
        //    speed = 2.5f;
        //}
    }

    void FixedUpdate()
    {

        desiredSpeed = Vector2.zero;
        Vector2 velocity = rb.linearVelocity;

        rb.linearVelocityY = -speed * Time.deltaTime * 100;

        if (rb.position.x + randomMove > 2.1f)
        {
            randomMove *= -1.0f;
        }
        if (rb.position.x + randomMove < -2.1f)
        {
            randomMove *= -1.0f;
        }

        rb.position = new Vector2(rb.position.x + randomMove, rb.position.y);
        rb.rotation += randomRotation;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            Destroy(gameObject);
        }
    }
}
