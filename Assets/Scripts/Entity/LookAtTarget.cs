using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class LookAtTarget : MonoBehaviour
{
    public float lerpSpeed = 1f;
    public bool targetIsPlayer;
    public Transform target;
    // public Rigidbody myRigidbody;
    public SpriteRenderer mySprite;
    public bool flipYIfUnder = false;
    void Start()
    {
        if(targetIsPlayer) {
            target = GameManager.playerTrans;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // rotation
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, angle)), lerpSpeed * Time.deltaTime);

        if(mySprite && flipYIfUnder == true) {
            if (target.position.x < transform.position.x) {
                mySprite.flipY = true;
            } else {
                mySprite.flipY = false;
            }
        }
        /*
        if (target.position.x < transform.position.x) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        */
    }
}
