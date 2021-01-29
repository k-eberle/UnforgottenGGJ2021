using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParalaxSprite : MonoBehaviour
{
    public float factor = 1.0f;

    private SpriteRenderer spriteRenderer;
    private Camera targetCamera;

    private Vector3 initialPosition;
    private Vector3 initialCameraPosition;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetCamera = Camera.main;

        initialPosition = transform.position;
        initialCameraPosition = targetCamera.transform.position;
    }

    private void Update()
    {
        Vector3 offset = factor * (targetCamera.transform.position - initialCameraPosition);
        Vector3 newPosition = initialPosition + new Vector3(offset.x, offset.y, 0);
        transform.position = newPosition;
    }
}
