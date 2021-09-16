using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameManager>().Miss();
    }

}
