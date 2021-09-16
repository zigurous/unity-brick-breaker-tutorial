using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetPaddle();
    }

    public void ResetPaddle()
    {
        this.rigidbody.velocity = Vector2.zero;
        this.transform.position = new Vector2(0f, this.transform.position.y);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this.direction = Vector2.left;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            this.direction = Vector2.right;
        } else {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (this.direction != Vector2.zero) {
            this.rigidbody.AddForce(this.direction * this.speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector2 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float maxOffset = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / maxOffset) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }

}
