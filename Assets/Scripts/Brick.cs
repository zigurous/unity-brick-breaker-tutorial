using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states = new Sprite[0];
    public int health { get; private set; }
    public int points = 100;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Restore();
    }

    public void Restore()
    {
        this.health = this.states.Length;
        this.spriteRenderer.sprite = this.states[this.health - 1];
        this.gameObject.SetActive(true);
    }

    private void Hit()
    {
        this.health--;

        if (this.health <= 0) {
            Break();
        } else {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }

        FindObjectOfType<GameManager>().Hit(this);
    }

    private void Break()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") {
            Hit();
        }
    }

}
