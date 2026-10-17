using UnityEngine;

public class BulletController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Vector3 direction;
    public float speed = 10f;

    // public TrailRenderer myTrail;
    private Rigidbody myRigidbody;
    void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable() {
        // myTrail.AddPosition(transform.position);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            return;
        }
        gameObject.SetActive(false);
    }
    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + direction * speed * Time.deltaTime);
    }
}
