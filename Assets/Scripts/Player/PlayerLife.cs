using Assets.Shared;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body;

#pragma warning disable 0649

    [SerializeField]
    private AudioSource DieAudio;

#pragma warning restore 0649

    private void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (body.position.y < -13)
        {
            Util.RestartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("death");
        body.bodyType = RigidbodyType2D.Static; // TODO: try to find a way to kill player that don't raise warnings

        DieAudio.Play();
    }

    private void RestartLevel()
    {
        Util.RestartLevel();
    }
}