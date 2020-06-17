using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem dust;
    
    // How much to smooth out the movement
    [Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;	
    // Amount of force added when the player jumps.
    [SerializeField] private float mJumpForce = 400f;
    // Whether or not a player can steer while jumping;
    [SerializeField] private bool mAirControl = true;

    // public CharacterController2D controller;
    public float horizontalMove;
    [SerializeField] public float runSpeed = 40f;
    private bool _jump;
    public bool crouch = true;
    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    // private static readonly int IsCrouching = Animator.StringToHash("IsCrouching");
    public AudioSource jumpSound;
    private Rigidbody2D _mRigidBody2D;
    private bool _mFacingRight = true;
    private Vector3 _mVelocity = Vector3.zero;
    public bool isGrounded = true;

    private void Start()
    {
        jumpSound = GetComponent<AudioSource>();
        _mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(Speed, Mathf.Abs(horizontalMove));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
            // animator.SetBool(IsJumping, true);    
        }
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, _jump);
        _jump = false;
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (isGrounded || mAirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, _mRigidBody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            _mRigidBody2D.velocity = Vector3.SmoothDamp(_mRigidBody2D.velocity, targetVelocity, ref _mVelocity, mMovementSmoothing);
    
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !_mFacingRight)
            {
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && _mFacingRight)
            {
                Flip();
            }
        }

        if (isGrounded && jump)
        {
            dust.Play();
            isGrounded = false;
            animator.SetBool(IsJumping, true);    
            _mRigidBody2D.AddForce(new Vector2(0f, mJumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _mFacingRight = !_mFacingRight;
        transform.Rotate(0f, 180f, 0f);
        dust.Play();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Tilemap") && other.otherCollider.CompareTag("PlayerLegs"))
        {
            animator.SetBool(IsJumping, false);
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            Finished.ShowFinishedText();
        }
    }
}