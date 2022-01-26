using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform pota;

    private Rigidbody rb;

    private Vector3 firstPos, lastPos, movePos;
    private bool isSafe, isGrounded, movingUp;
    private float time;
    public float throwSpeed;
    public float moveSpeed, upSpeed;

    private float dikeyAci;
    private float yatayAci;
    private float distance;
    private float xzDistance;
    private float yDistance;
    private float xDistance;
    private float zDistance;

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
                    Vector3 direction = new Vector3(pota.position.x - transform.position.x, pota.position.y - transform.position.y, pota.position.z - transform.position.z);
                    dikeyAci = Vector3.Angle(direction, Vector3.forward);
                    yatayAci = Vector3.Angle(direction, Vector3.left);
                    distance = Vector3.Distance(transform.position, pota.position);

                    if (yatayAci < 90 && dikeyAci < 90)
                    {
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = (xzDistance * Mathf.Cos(yatayAci));
                        zDistance = Mathf.Abs(xzDistance * Mathf.Sin(yatayAci));

                        if (Mathf.Sign(xDistance) == 1)
                        {
                            xDistance = -xDistance;
                        }

                        Debug.Log("1. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }

                    else if (yatayAci > 90 && dikeyAci < 90)
                    {
                        yatayAci = Vector3.Angle(direction, Vector3.right);
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = Mathf.Abs(xzDistance * Mathf.Cos(yatayAci));
                        zDistance = Mathf.Abs(xzDistance * Mathf.Sin(yatayAci));

                        Debug.Log("2. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }

                    else if (yatayAci > 90 && dikeyAci > 90)
                    {
                        yatayAci = Vector3.Angle(direction, Vector3.right);
                        dikeyAci = Vector3.Angle(direction, Vector3.back);
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = Mathf.Abs(xzDistance * Mathf.Cos(yatayAci));
                        zDistance = (xzDistance * Mathf.Sin(yatayAci));

                        if (Mathf.Sign(zDistance) == 1)
                        {
                            zDistance = -zDistance;
                        }

                        Debug.Log("3. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }

                    else if (yatayAci < 90 && dikeyAci > 90)
                    {
                        dikeyAci = Vector3.Angle(direction, Vector3.back);
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = (xzDistance * Mathf.Cos(yatayAci));
                        zDistance = (xzDistance * Mathf.Sin(yatayAci));

                        if (Mathf.Sign(xDistance) == 1)
                        {
                            xDistance = -xDistance;
                        }

                        if (Mathf.Sign(zDistance) == 1)
                        {
                            zDistance = -zDistance;
                        }

                        Debug.Log("4. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }

                    else if (yatayAci == 90 && dikeyAci < 90)
                    {
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = 0;
                        zDistance = Mathf.Abs(xzDistance * Mathf.Sin(yatayAci));

                        Debug.Log("5. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }

                    else if (yatayAci == 90 && dikeyAci > 90)
                    {
                        xzDistance = Mathf.Abs(distance * Mathf.Cos(dikeyAci));
                        yDistance = Mathf.Abs(distance * Mathf.Sin(dikeyAci));
                        xDistance = 0;
                        zDistance = (xzDistance * Mathf.Sin(yatayAci));

                        if (Mathf.Sign(zDistance) == 1)
                        {
                            zDistance = -zDistance;
                        }

                        Debug.Log("6. IF\nDikey Açý : " + dikeyAci + ",\t Yatay Açý : " + yatayAci + ",\t Uzaklýk : " + distance + ",\t Y Uzaklýk : " + yDistance +
                       ",\t XZ Uzaklýk : " + xzDistance + ",\t X Uzaklýk : " + xDistance + ",\t Z Uzaklýk : " + zDistance);
                    }
                    float flightTime = (yDistance / -Physics.gravity.y) * 2;
                    float xSpeed = xDistance / flightTime;
                    float zSpeed = zDistance / flightTime;
                    Debug.Log("Ucus Suresi : " + flightTime + ",X Speed : " + xSpeed + ",Z Speed : " + zSpeed);
                    //rb.AddForce(new Vector3(0, yDistance*3, zDistance), ForceMode.Impulse);
                    rb.velocity = new Vector3(xSpeed, yDistance*4, zSpeed);
                }
            }
        }

        else
        {
            if (isGrounded)
            {
                distance = 0;
                xzDistance = 0;
                yDistance = 0;
                xDistance = 0;
                zDistance = 0;
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
                xzDistance = 0;
                yDistance = 0;
                xDistance = 0;
                zDistance = 0;
                time = 0;
                firstPos = Vector3.zero;
                lastPos = Vector3.zero;
                movePos = Vector3.zero;
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            }

            else if (!isGrounded && isSafe)
            {
                distance = 0;
                xzDistance = 0;
                yDistance = 0;
                xDistance = 0;
                zDistance = 0;
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


