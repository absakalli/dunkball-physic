using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform pota;

    private Rigidbody rb;

    public Vector3 firstPos, lastPos, movePos;
    public bool isSafe, isGrounded;
    public float time, throwSpeed, directSpeed;
    public float moveSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSafe = false;
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
            lastPos = Input.mousePosition;
            movePos = lastPos - firstPos;
            rb.AddForce(new Vector3(Mathf.Clamp(movePos.x, -1, 1), 0, Mathf.Clamp(movePos.y, -1, 1)) * moveSpeed);
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
                    Vector3 direction = (pota.transform.position - transform.position);
                    directSpeed = direction.magnitude;
                    Debug.Log("DIRECTION : " + direction + " SPEED : " + directSpeed);
                    rb.AddForce(direction * directSpeed, ForceMode.Impulse);
                }
            }
        }

        else
        {
            if (isGrounded)
            {
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                if (transform.position.y < 0.51f)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, 0);
                }
                else
                {
                    rb.AddForceAtPosition(new Vector3(0, -rb.velocity.y, 0), new Vector3(transform.position.x, 0.5f, transform.position.z), ForceMode.Impulse);
                }
            }
            else
            {
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
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
        }
    }
}


