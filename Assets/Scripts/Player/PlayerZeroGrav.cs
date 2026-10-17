using System.Collections;
using UnityEngine;

public class PlayerZeroGrav : MonoBehaviour
{
    /* Alexis Clay Drain */
    public LineRenderer crosshairLine;
    public GameObject lookingAtObject;
    public GameObject attachedObject;
    public Transform targetHold;
    public AudioClip shootSFX;
    public AudioClip failSFX;
    public float shootForce = 5f;

    void Start() {

    }
    RaycastHit hit;
    public void Update() {

        Physics.Raycast(GameManager.playerTrans.position, GameManager.playerTrans.forward, out hit, 8f);
        if(hit.collider && hit.collider.GetComponent<Moveable>()) {
            lookingAtObject = hit.collider.gameObject;
            crosshairLine.startColor = new Color(0f, 0.66f, 1f);
        } else {
            lookingAtObject = null;
            crosshairLine.startColor = Color.white;
        }

        if(GameManager.playerActionsControls.Player.ZeroGrav.WasPressedThisFrame()) {
            if (attachedObject != null) {
                attachedObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                attachedObject.GetComponent<Rigidbody>().AddForce(GameManager.playerTrans.forward * shootForce, ForceMode.Impulse);
                attachedObject = null;
                GameManager.soundManager.SpawnLoudAudio(shootSFX);
            } else {
                if(lookingAtObject) {
                    attachedObject = lookingAtObject;
                } else {
                    GameManager.soundManager.SpawnLoudAudio(failSFX);
                }
            }

        }
    }
    public void FixedUpdate() {
        if (attachedObject == null) {
            return;
        }
        // attachedObject.GetComponent<Rigidbody>().AddForce(dir * speed * Time.deltaTime);
        attachedObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(attachedObject.transform.position, targetHold.position, 0.15f));
        attachedObject.GetComponent<Rigidbody>().MoveRotation(targetHold.rotation);
    }
}
