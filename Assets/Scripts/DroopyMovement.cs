using UnityEngine;
using System.Collections;
using TMPro;

public class DroopyMovement : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public bool isGrounded;
    public bool isHeadHit;
    public bool isStarted;
    public float jumpForce;
    private bool facingRight = true;
    private bool secondJump = true;
    private bool canTakeDamage = true;
    public Animator animator;

    public AudioSource dash;
    public AudioSource jump;
    public AudioSource run;

    private bool isWalkingSound = false;

    public GameObject map;
    private bool isMapOpen = false;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 10f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }


    void Update()
    {
        if (isDashing)
        {
            return;
        }
        float horizontal = 0f;
        Vector2 direction = new Vector2(0, rb.velocity.y);



        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
            if (!isWalkingSound && isGrounded == true)
            {
                run.Play();
                isWalkingSound = true;
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
            if (!isWalkingSound && isGrounded == true)
            {
                run.Play();
                isWalkingSound = true;
            }
        }
        else
        {
            horizontal = 0;
            if (isWalkingSound || isGrounded == false)
            {
                run.Stop();
                isWalkingSound = false;
            }
        }

        if (horizontal > 0)
        {
            direction = new Vector2(speed, rb.velocity.y);

            if (facingRight)
            {

            }
            else if (!facingRight)
            {
                flip();
            }


        }

        else if (horizontal < 0)
        {
            direction = new Vector2(-speed, rb.velocity.y);

            if (facingRight)
            {
                flip();
            }
            else if (!facingRight)
            {

            }
        }

        rb.velocity = direction;
        animator.SetFloat("horizontalSpeed", Mathf.Abs(direction.x));
        animator.SetFloat("verticalSpeed", direction.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                animator.SetBool("jumping", true);
                jump.Play();
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (secondJump)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("jumping", true);
                jump.Play();
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                secondJump = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M pressed");
            if (isMapOpen == false)
            {
                map.gameObject.SetActive(true);
                isMapOpen = true;
            }
            else
            {
                map.gameObject.SetActive(false);
                isMapOpen = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            dash.Play();
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    public void SetGround(bool _ground)
    {
        isGrounded = _ground;
        if (isGrounded)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("grounded", true);
            secondJump = true;
        }
        else
        {
            animator.SetBool("grounded", false);
        }
    }

    public void HeadHit(bool headHit)
    {
        isHeadHit = headHit;
        if (isHeadHit)
        {
            animator.SetBool("headHit", true);
        }
        else
        {
            StartCoroutine(WaitForHeadHit());
        }
    }

    private IEnumerator WaitForHeadHit()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("headHit", false);
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1f;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            StartCoroutine(takeDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Health"))
        {
            gameObject.GetComponent<DroopyHealth>().RestoreHealth(50);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Enemy") && canTakeDamage)
        {
            StartCoroutine(takeDamage());
        }
    }

    IEnumerator takeDamage()
    {
        canTakeDamage = false;
        gameObject.GetComponent<DroopyHealth>().TakeDamage(20);
        yield return new WaitForSeconds(1);
        canTakeDamage = true;

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
