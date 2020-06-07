using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float mJumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float mCrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float mMovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool mAirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask mWhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform mGroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform mCeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D mCrouchDisableCollider;				// A collider that will be disabled when crouching

	const float KGroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool _mGrounded;            // Whether or not the player is grounded.
	const float KCeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D _mRigidbody2D;
	private bool _mFacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 _mVelocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		_mRigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = _mGrounded;
		_mGrounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(mGroundCheck.position, KGroundedRadius, mWhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
	
			if (colliders[i].gameObject != gameObject)
			{
				Debug.Log("CC we hit something marked as ground: " + colliders[i].tag);
				_mGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(mCeilingCheck.position, KCeilingRadius, mWhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (_mGrounded || mAirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= mCrouchSpeed;

				// Disable one of the colliders when crouching
				if (mCrouchDisableCollider != null)
					mCrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (mCrouchDisableCollider != null)
					mCrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, _mRigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			_mRigidbody2D.velocity = Vector3.SmoothDamp(_mRigidbody2D.velocity, targetVelocity, ref _mVelocity, mMovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !_mFacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && _mFacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (_mGrounded && jump)
		{
			// Add a vertical force to the player.
			Debug.Log("CC Jump, add force");
			_mGrounded = false;
			_mRigidbody2D.AddForce(new Vector2(0f, mJumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		_mFacingRight = !_mFacingRight;
		transform.Rotate(0f, 180f, 0f);
	}
}
