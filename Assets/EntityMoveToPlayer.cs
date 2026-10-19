using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class EntityMoveToPlayer : MonoBehaviour
{

    public float seeDistance = 10f;
    public float movementSpeed = 0.1f;

    private Rigidbody myRigidbody;
    private bool goToPlayer = false;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    RaycastHit hit;
    void FixedUpdate()
    {
        if(goToPlayer == true) {
            myRigidbody.MovePosition(Vector3.MoveTowards(myRigidbody.position, GameManager.playerTrans.position, movementSpeed));
        }
        if(goToPlayer == false) {
            if (Vector2.Distance(transform.position, GameManager.playerTrans.position) < seeDistance) {
                Physics.Linecast(transform.position, GameManager.playerTrans.position, out hit);
                if (hit.collider.CompareTag("Player") == false) {
                    return;
                }

                goToPlayer = true;
                // play found player SFX here

            }
        }
        
    }

}
