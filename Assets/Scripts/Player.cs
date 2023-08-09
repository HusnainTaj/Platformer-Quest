using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int FinalLevel;

    private Rigidbody2D playerRigibody;
    private Animator playerAnimator;

    bool facingRight = true;
    bool isGrounded;
    bool onLadder;
    bool jump;
    bool duck;

    [SerializeField]
    private float playerWalkSpeed;

    [SerializeField]
    private float playerClimbSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private float invincibilityTime = 2;

    private int gems = 0;

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;

    private GameManager GM;

    void Start ()
    {
        playerRigibody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        GM = FindObjectOfType<GameManager>();

        // Making sure that play and enemies collides with each other *(Bug Fix)*
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
    }

    void Update()
    {
        HandleInput();

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y);
    }

    void FixedUpdate ()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        isGrounded = IsGrounded();

        HandleMovement(horizontalAxis, verticalAxis);

        Flip(horizontalAxis);
	}

    void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
            jump = true;

        if (Input.GetKey(KeyCode.DownArrow))
            duck = true;
        else
            duck = false;
    }

    void HandleMovement(float horizontalAxis, float verticalAxis)
    {
        if(!duck)
            playerRigibody.velocity = new Vector2(horizontalAxis * playerWalkSpeed, playerRigibody.velocity.y);

        playerAnimator.SetFloat("walkSpeed", Mathf.Abs(horizontalAxis));

        // Making player jump if player is standing on ground not on the ladder and jump btn(space bar) is pressed
        if (isGrounded && jump && !onLadder && !duck)
        {
			jump = false;
            isGrounded = false;
            playerRigibody.AddForce(new Vector2(0, jumpForce));
        }

        // If player is not standing on ground playing jump animation
        if (!isGrounded)
            playerAnimator.SetBool("isJumping", true);
        else
            playerAnimator.SetBool("isJumping", false);

        // If Player is on Ladder making him able to fly up & down
        if (onLadder)
        {
            playerRigibody.velocity = new Vector2(horizontalAxis * playerClimbSpeed, verticalAxis * playerClimbSpeed);

            playerAnimator.SetFloat("climbSpeed", Mathf.Abs(verticalAxis));
        }
        else
            playerAnimator.SetFloat("climbSpeed", 0);

        // Playing duck animation if player is not on ladder & pressed duck btn(down arrow)
        if (duck && !onLadder)
            playerAnimator.SetBool("isDucking", true);
        else
            playerAnimator.SetBool("isDucking", false);

    }

    void Flip(float horizontalAxis)
    {
        if (horizontalAxis > 0 && !facingRight || horizontalAxis < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    bool IsGrounded()
    {
        if (playerRigibody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void Hurt(float Damage)
    {
        HealthBar.decreaseHealth(Damage);
        StartCoroutine(invincible());
    }
    
    IEnumerator invincible()
    {
        // Ignore collision with enemies
        int playerLayer = LayerMask.NameToLayer("Player");
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        // Start blink Anim
        playerAnimator.SetLayerWeight(1, 1);

        // Wait For invincibilityTime to end
        yield return new WaitForSeconds(invincibilityTime);

        // Stop blibking anim & re-enable colllisions
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        playerAnimator.SetLayerWeight(1, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            collider.transform.parent.GetComponent<Collider2D>().enabled = false;
            playerRigibody.gravityScale = 0;
            playerAnimator.SetBool("onLadder", true);
            onLadder = true;
        }

        if (collider.gameObject.tag == "DoorOpened")
        {
            GM.Win(playerAnimator, playerRigibody, gems);
        }

        if (collider.gameObject.tag == "Gem")
        {
            FindObjectOfType<AudioManager>().Play("Gem");
            collider.gameObject.SetActive(false);
            gems++;
        }

        if (collider.gameObject.tag == "Life")
        {
            collider.gameObject.SetActive(false);

            if (collider.GetComponent<SpriteRenderer>().sprite.name == "LifeHalf")
                HealthBar.increaseHealth(5f);
            else
                HealthBar.increaseHealth(10f);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
            collider.transform.parent.GetComponent<Collider2D>().enabled = true;
            playerRigibody.gravityScale = 1;
            playerAnimator.SetBool("onLadder", false);
            onLadder = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "DeadEnd")
        {
            HealthBar.decreaseHealth(500);
        }
    }
}