using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fruit")
        {
            GameManager.Instance.AddScore(1);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Poop")
        {
            GameManager.Instance.GameOver();
            Destroy(collision.gameObject);
        }
    }
}
