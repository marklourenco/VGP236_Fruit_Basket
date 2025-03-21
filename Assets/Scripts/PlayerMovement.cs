using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb = null;

    private Vector2 desiredSpeed = Vector2.zero;

    private float maxMoveLeft = -2.5f;
    private float maxMoveRight = 2.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.position = new Vector2(0.0f, -3.5f);
    }

    void FixedUpdate()
    {
    }

    public void MoveLeft()
    {
        if (rb.position.x + -0.5 < maxMoveLeft)
        {
            rb.position = new Vector2(maxMoveLeft, rb.position.y);
            Debug.Log(desiredSpeed.x);
            return;
        }
        rb.position = new Vector2(rb.position.x - 0.5f, rb.position.y);
    }
    public void MoveRight()
    {
        if (rb.position.x + 0.5 > maxMoveRight)
        {
            rb.position = new Vector2(maxMoveRight, rb.position.y);
            Debug.Log(desiredSpeed.x);
            return;
        }
        rb.position = new Vector2(rb.position.x + 0.5f, rb.position.y);
    }
}