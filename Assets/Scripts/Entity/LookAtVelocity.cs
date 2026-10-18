using UnityEngine;

public class LookAtVelocity : MonoBehaviour
{
    public float lerpSpeed = 1f;
    public Rigidbody myRigidbody;
    private SpriteRenderer mySprite;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // rotation
        float angle = Mathf.Atan2(myRigidbody.linearVelocity.y, myRigidbody.linearVelocity.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), lerpSpeed * Time.deltaTime);

        if(mySprite) {
            if(myRigidbody.linearVelocity.x < -1f) {
                mySprite.flipY = true;
            } else {
                mySprite.flipY = false;
            }
        }
    }
}
