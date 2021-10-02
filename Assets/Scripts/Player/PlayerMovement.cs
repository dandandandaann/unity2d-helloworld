using Assets.Scripts.Models;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body { get; set; }
    private BoxCollider2D coll { get; set; }
    private Animator animator { get; set; }
    private SpriteRenderer sprite { get; set; }

    private float currentX = 0f;
    private bool hasJumped = false;

    #region Serialized
#pragma warning disable 0649

    [SerializeField]
    private float MoveSpeed = 7f;
    [SerializeField]
    private float JumpForce = 5f;
    [SerializeField]
    private float JumpMaxHeight = 5f;
    [SerializeField]
    private LayerMask JumpableGround;
    [SerializeField]
    private AudioSource JumpAudio;

#pragma warning restore 0649
    #endregion

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (IsOnSkate)
        {
            var skateBody = transform.parent.GetComponent<Rigidbody2D>();
            Move(skateBody);
            //body.rotation = skateBody.rotation;
            //Move(body);
        }
        else
        { 
            Move(body);
            
            //JumpSimplest(IsGrounded);

            JumpPushDown(IsGrounded);

            // MarioJump();
            // MarioJumpFixed();
            //JumpFromVideo();

            UpdateAnimationState();
        }
    }

    private void Move(Rigidbody2D body2D)
    {
        currentX = Input.GetAxisRaw(Axis.Horizontal);
        body2D.velocity = new Vector2(currentX * MoveSpeed, body2D.velocity.y);
    }
    private void JumpSimplest(bool isGrounded)
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, JumpForce);
            JumpAudio.Play();
        }
    }

    public float minimumJumpVelocity = 0f;
    public float JumpDownForce = 5f;
    private bool jumpButtonDown = false;

    private void JumpPushDown(bool isGrounded)
    {
        if (Input.GetButtonUp("Jump"))
            jumpButtonDown = false;
        if (Input.GetButtonDown("Jump"))
            jumpButtonDown = true;

        if (jumpButtonDown && isGrounded)
        {
            hasJumped = true;
            body.velocity = new Vector2(body.velocity.x, JumpForce);
            JumpAudio.Play();
        }

        if (hasJumped && (!jumpButtonDown && body.velocity.y < minimumJumpVelocity) || (jumpButtonDown && body.velocity.y < 0))
        {
            hasJumped = false;
            body.velocity = new Vector2(body.velocity.x, -JumpDownForce);
        }
    }


    #region TODO: fix jump
    [SerializeField]
    public float jumpMaxTime;

    float currentJumptime = 0; // TODO:
    bool isJumping = true; // TODO
    float fallMultiplier = 1f, jumpMultiplier = 1f; // TODO ?

    private void JumpFromVideo()
    {
        var gravityY = Physics.gravity.y;


        if (body.velocity.y > 0 && isJumping)
        {
            currentJumptime += Time.deltaTime;

            // player jump rising
            float t = currentJumptime / jumpMaxTime * 1;
            float tempJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                tempJumpM = jumpMultiplier * (1 - t);
            }

            body.velocity = new Vector2(body.velocity.x, gravityY * tempJumpM * Time.deltaTime);
            //body.velocity.y += gravityY * tempJumpM * Time.deltaTime;
        }
        else if (body.velocity.y < 0)
        {
            //player jump fallng
            body.velocity = new Vector2(body.velocity.x, gravityY * fallMultiplier * Time.deltaTime);
            //body.velocity.y += gravityY * fallMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            currentJumptime = 0f;
            body.velocity = new Vector2(body.velocity.x, JumpForce);
            JumpAudio.Play();
        }
    }

    private bool AlternativeIsGround() 
    {
        // TODO: try this alternative check
        // it needs a feet collider
        // can a BoxCollider not interect with other colliders? So it's just used as a ground check

        float groundCheckRadius = 0.7f;
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        return  Physics2D.OverlapCircle(transform.position, groundCheckRadius, JumpableGround);

    }

    #endregion

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.Idle;

        if (currentX > 0f)
        {
            state = MovementState.Run;
            sprite.flipX = false;
        }
        else if (currentX < 0f)
        {
            state = MovementState.Run;
            sprite.flipX = true;
        }

        if (body.velocity.y > .1f)
        {
            state = MovementState.Jump;
        }
        else if (body.velocity.y < -.1f)
        {
            state = MovementState.Fall;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded => Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround).collider != null;
    private bool IsOnSkate => transform.parent?.name == "Skate";
}