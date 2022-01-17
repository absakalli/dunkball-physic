using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform pota;

    private Rigidbody rb;

    public Vector3 firstPos, lastPos, movePos;
    public bool isSafe, isGrounded, movingUp;
    public float time, throwSpeed, directSpeed;
    public float moveSpeed, upSpeed;
    public ForceMode force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSafe = false;
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        Vector3 lookattarget = pota.position;
        lookattarget.y = 0;
        transform.LookAt(lookattarget);
        if(movingUp && transform.position.y < 0.51f)
        {
            float diff = 0.51f - transform.position.y;
            //float speedDiff=rb.velocity.magnitude
            rb.AddForce(Vector3.up * upSpeed * diff, force);
        }
        if (transform.position.y>= 0.51f)
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
            Vector3 targetVector = transform.right * movePos.x + transform.forward * movePos.y;
            targetVector.y = 0;
            Debug.Log(targetVector);
            rb.AddForce(targetVector * moveSpeed);
            //rb.AddForce(new Vector3(movePos.x, 0, movePos.y) * moveSpeed);
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
                    //Vector3 direction = new Vector3((pota.position.x - transform.position.x), (pota.position.y - transform.position.y)*1.5f, (pota.position.z - transform.position.z));
                    //directSpeed = direction.magnitude/4;
                    //Debug.Log("DIRECTION : " + direction + " SPEED : " + directSpeed);
                    //rb.AddForce(direction * directSpeed, ForceMode.Impulse);
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
            movingUp = true;
        }
        else
        {
            isGrounded = false;
            movingUp = false;
        }
    }
}


