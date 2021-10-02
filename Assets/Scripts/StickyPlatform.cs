using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    [SerializeField]
    public BoxCollider2D stickyCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == stickyCollider && collision.gameObject.IsPlayer())
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider == stickyCollider && collision.gameObject.IsPlayer())
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
