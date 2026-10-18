using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    public UnityEvent onTouchEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("Player")) {
            onTouchEvent.Invoke();
        }
    }
    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            onTouchEvent.Invoke();
        }
    }
}
