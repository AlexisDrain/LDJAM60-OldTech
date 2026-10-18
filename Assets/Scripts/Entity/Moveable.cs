using UnityEngine;

public class Moveable : MonoBehaviour
{
    /* Alexis Clay Drain */
    public bool battery = false;
    public void OnCollisionEnter(Collision collision) {
        if (GetComponent<Rigidbody>().linearVelocity.magnitude > 2f) {
            if(GetComponent<EntityHealth>()) {
                GetComponent<EntityHealth>().DoDamage(10);
            }
            if (collision.collider.GetComponent<EntityHealth>()) {
                collision.collider.GetComponent<EntityHealth>().DoDamage(10);
            }
        }
    }
}
