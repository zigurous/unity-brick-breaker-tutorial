using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerZone : MonoBehaviour
{
    public UnityEvent onEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.onEnter != null) {
            this.onEnter.Invoke();
        }
    }

}
