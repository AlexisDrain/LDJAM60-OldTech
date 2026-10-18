using UnityEngine;

public class BulletController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Vector3 direction;
    public float speed = 10f;
    public int damage = 10;
    public bool hurtPlayer = false;
    public bool hurtEnemy = false;
    // public TrailRenderer myTrail;
    private Rigidbody myRigidbody;
    void Awake() {
        myRigidbody = GetComponent<Rigidbody>();
    }
    private void OnEnable() {
        // myTrail.AddPosition(transform.position);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet") ||
            (hurtPlayer == false && other.CompareTag("Player")) ||
            (hurtEnemy == false && other.CompareTag("Enemy"))) {
            return;
        }

        if (other.GetComponent<EntityHealth>()) {
            other.GetComponent<EntityHealth>().DoDamage(damage);
        }
        if (hurtPlayer == true && other.GetComponent <PlayerHealth>()) {
            other.GetComponent<PlayerHealth>().DoDamage(damage);
        }
        gameObject.SetActive(false);
    }
    private void FixedUpdate() {
        myRigidbody.MovePosition(myRigidbody.position + direction * speed * Time.deltaTime);
    }
}
