using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform pota;

    private Rigidbody rb;

    private Vector3 firstPos, lastPos, movePos;
    private bool isSafe, isGrounded, movingUp;
    private float time;
    private float distance;
    public float throwSpeed;
    public float moveSpeed, upSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSafe = false;
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (movingUp && transform.position.y < 0.51f)
        {
            float diff = 0.51f - transform.position.y;
            rb.AddForce(Vector3.up * upSpeed * diff, ForceMode.Force);
        }

        if (transform.position.y >= 0.51f)
        {
            movingUp = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (firstPos == Vector3.zero)
            {
                firstPos = Input.mousePosition;
            }
            time += Time.deltaTime;
            lastPos = Input.mousePosition;
            movePos = lastPos - firstPos;
            movePos.Normalize();
            rb.AddForce(new Vector3(movePos.x, 0, movePos.y) * moveSpeed);
        }

        else if (Input.GetMouseButtonUp(0) && movePos.y > 0 && time < 0.6f)
        {
            if (isGrounded)
            {
                isGrounded = false;

                if (!isSafe)
                {
                    rb.AddForce(new Vector3(0, throwSpeed, throwSpeed), ForceMode.Impulse);
                }

                else
                {
                    Vector3 direction = new Vector3(pota.position.x - transform.position.x, pota.position.y - 0, pota.position.z - transform.position.z).normalized;
                    distance = Vector3.Distance(transform.position, pota.position);
                    rb.AddForce(new Vector3(0, 3.5f, 0),ForceMode.Impulse);
                    rb.velocity = direction*distance*1.3f;
                }
            }
        }

        else
        {
            if (isGrounded)
            {
                distance = 0;
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                if (transform.position.y < 0.51f)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }
            }

            else if (!isGrounded && !isSafe)
            {
                distance = 0;
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }

            else if (!isGrounded && isSafe)
            {
                distance = 0;
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Safe")
        {
            isSafe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Safe")
        {
            isSafe = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
            movingUp = true;
        }
        else
        {
            isGrounded = false;
            movingUp = false;
        }
    }
}


