using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public float leadingDistance;
    public float smoothTime;

    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        if (!player)
        {
            Debug.LogError("CameraController: Link to Player not set!");
            this.enabled = false;
        }
    }

    private void Update()
    {
#if false
        Vector2 target = (Vector2)player.transform.position + leadingDistance * player.GetVelocity().normalized;
        Vector3 nextPosition = Vector3.SmoothDamp(transform.position, new Vector3(target.x, target.y, transform.position.z), ref velocity, smoothTime);
        transform.position = nextPosition;
#endif

        if (!player.IsDying())
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
