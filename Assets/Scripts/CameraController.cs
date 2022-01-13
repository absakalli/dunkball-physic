using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public Transform pota;

    private Vector3 offset;

    void Start()
    {
        offset.x = transform.position.x - player.position.x;
        offset.y = transform.position.y;
        offset.z = transform.position.z - player.position.z;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + offset.x, offset.y, player.position.z + offset.z);
        transform.DODynamicLookAt(new Vector3((pota.position.x + player.position.x)/2, transform.position.y, (pota.position.z + player.position.z) / 2), 0.5f);
    }
}