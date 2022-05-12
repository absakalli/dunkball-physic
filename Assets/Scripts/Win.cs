using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] public GameObject konfeti, konfeti2, canvas;
    private Transform parent, asagi;

    BallController ballController;

    private bool isTrigger;

    private void Awake()
    {
        ballController = GameObject.FindObjectOfType<BallController>();
    }

    private void Start()
    {
        parent = gameObject.transform.parent;
        asagi = parent.transform.GetChild(1);
        asagi.GetComponent<Collider>().isTrigger = false;
        isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isTrigger)
        {
            asagi.GetComponent<Collider>().isTrigger = true;
            Instantiate(konfeti, transform.position + new Vector3(.6f, .6f, 1), Quaternion.Euler(-60, 90, 0));
            Instantiate(konfeti, transform.position + new Vector3(-.6f, .6f, 1), Quaternion.Euler(-120, 90, 0));
            Instantiate(konfeti2, transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            isTrigger = false;
            ballController.isMove = false;
            canvas.SetActive(true);
        }
    }
}
