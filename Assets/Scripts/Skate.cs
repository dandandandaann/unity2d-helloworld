using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Transform PlayerTransform { get; set; }
    private Rigidbody2D PlayerBody { get; set; }
    private GameObject Player { get; set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.parent != transform && collision.gameObject.IsPlayer())
        {
            Player = collision.gameObject;
            PlayerTransform = collision.gameObject.transform;
            PlayerBody = collision.gameObject.GetComponent<Rigidbody2D>();

            PlayerTransform.SetParent(transform);

            // TODO: check if player is on top before positioning
            PlayerBody.freezeRotation = false;
            Invoke("Mount", 0.1f);
        }
    }

    private void Mount()
    {
        PlayerTransform.position = new Vector2(transform.position.x, transform.position.y + 1.08f);
        PlayerTransform.rotation = transform.rotation;

        PlayerBody.velocity = new Vector2(0f, 0f);
        PlayerBody.angularVelocity = 0f;
        PlayerBody.isKinematic = true;

        Player.GetComponent<SpriteRenderer>().flipX = false;
        Player.GetComponent<Animator>().SetInteger("state", (int)MovementState.Idle);
    }
}