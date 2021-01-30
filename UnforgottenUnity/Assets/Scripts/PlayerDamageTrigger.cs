using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDamageTrigger : MonoBehaviour
{
    private void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (!collider.isTrigger)
            Debug.LogError("PlayerDamageTrigger: Collider must be trigger!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            if (controller)
                controller.Damage();
        }
    }
}
