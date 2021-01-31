using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite unactivatedSprite;
    public Sprite activatedSprite;
    public GameObject lightSource;


    private void Awake()
    {
        spriteRenderer.sprite = unactivatedSprite;
        lightSource.SetActive(false);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = activatedSprite;
            lightSource.SetActive(true);


            PlayerPrefs.SetInt("LoadSave", 1);
            PlayerPrefs.SetFloat("SaveX", transform.position.x);
            PlayerPrefs.SetFloat("SaveY", transform.position.y);
            PlayerPrefs.SetFloat("SaveZ", transform.position.z);
        }

    }
}
