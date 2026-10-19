using UnityEngine;

public class Moveable : MonoBehaviour
{
    /* Alexis Clay Drain */
    public bool battery = false;
    public bool hurtOnNextImpact = false;
    public float hurtOnMagnatude = 1f;

    public void OnCollisionEnter(Collision collision) {
        if (hurtOnNextImpact == true && GetComponent<Rigidbody>().linearVelocity.magnitude > hurtOnMagnatude) {
            hurtOnNextImpact = false;
            if (GetComponent<EntityHealth>()) {
                GetComponent<EntityHealth>().DoDamage(10);
            }
            if (collision.collider.GetComponent<EntityHealth>()) {
                collision.collider.GetComponent<EntityHealth>().DoDamage(10);
            }
        }
    }
}
