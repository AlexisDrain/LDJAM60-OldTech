using DigitalRuby.LightningBolt;
using System.Collections;
using UnityEngine;

public class PlayerZeroGrav : MonoBehaviour
{
    /* Alexis Clay Drain */
    public LineRenderer crosshairLine;
    public LightningBoltScript lightningLine;
    public GameObject lookingAtObject;
    public GameObject attachedObject;
    public Transform targetHold;
    public AudioClip shootSFX;
    public AudioClip failSFX;
    public float shootForce = 5f;
    private AudioSource holdLoopAudio;
    void Start() {
        holdLoopAudio = GetComponent<AudioSource>();
    }
    public void RemoveAttachedObject() {
        attachedObject = null;

        holdLoopAudio.Stop();
        lightningLine.StartPosition = Vector3.zero;
        lightningLine.EndPosition = Vector3.zero;
        if (GameManager.tutorial_GrabObject) {
            GameManager.tutorial_GrabObject.SetActive(false);
        }
    }
    private void OnDisable() {
        RemoveAttachedObject();
    }
    RaycastHit hit;
    public void Update() {

        if(GameManager.gameIsPaused == true || GameManager.playerIsAlive == false) {
            return;
        }

        Physics.Raycast(GameManager.playerTrans.position, GameManager.playerTrans.forward, out hit, 8f);
        if(hit.collider && (hit.collider.GetComponent<Moveable>() || hit.collider.GetComponent<ItemPickup>())) {
            lookingAtObject = hit.collider.gameObject;
            crosshairLine.startColor = new Color(0f, 0.66f, 1f);
            if(attachedObject == null) {
                GameManager.tutorial_GrabObject.SetActive(true);
            } else {
                GameManager.tutorial_GrabObject.SetActive(false);
            }
        } else {
            lookingAtObject = null;
            crosshairLine.startColor = Color.white;
            GameManager.tutorial_GrabObject.SetActive(false);
        }

        if(GameManager.playerActionsControls.Player.Shoot.WasPressedThisFrame()) {
            if (attachedObject != null) {
                attachedObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                attachedObject.GetComponent<Rigidbody>().AddForce(GameManager.playerTrans.forward * shootForce, ForceMode.Impulse);
                attachedObject.GetComponent<Moveable>().hurtOnNextImpact = true;
                attachedObject = null;
                GameManager.soundManager.SpawnLoudAudio(shootSFX);
            } else {
                if(lookingAtObject) {
                    if(lookingAtObject.GetComponent<ItemPickup>()) {
                        lookingAtObject.GetComponent<ItemPickup>().TryPickup();
                    }
                    if (lookingAtObject.GetComponent<Moveable>()) {
                        attachedObject = lookingAtObject;
                    }
                } else {
                    GameManager.soundManager.SpawnLoudAudio(failSFX);
                }
            }

        }
    }
    public void FixedUpdate() {
        if (attachedObject == null) {
            if (holdLoopAudio.isPlaying == true) {
                holdLoopAudio.Stop();
                lightningLine.StartPosition = Vector3.zero;
                lightningLine.EndPosition = Vector3.zero;
            }
            return;
        }
        if (holdLoopAudio.isPlaying == false) {
            holdLoopAudio.Play();
            lightningLine.enabled = true;
        }
        lightningLine.StartPosition = transform.position;
        lightningLine.EndPosition = attachedObject.transform.position;
        // attachedObject.GetComponent<Rigidbody>().AddForce(dir * speed * Time.deltaTime);
        attachedObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(attachedObject.transform.position, targetHold.position, 0.2f));
        attachedObject.GetComponent<Rigidbody>().MoveRotation(targetHold.rotation);
    }
}
