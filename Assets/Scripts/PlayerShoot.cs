using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    /* Alexis Clay Drain */
    public AudioClip shootSFX;
    public Light light;
    void Start() {
        light = GetComponent<Light>();
    }
    public void Update() {
        if(GameManager.playerActionsControls.Player.Shoot.WasPressedThisFrame()) {
            GameManager.soundManager.SpawnLoudAudio(shootSFX);
            GameManager.playerTrans.GetComponent<Rigidbody>().AddForce(-GameManager.playerTrans.forward * 2f, ForceMode.Impulse);
            StartCoroutine(ShootCoroutine());
        }
    }
    private IEnumerator ShootCoroutine() {
        light.enabled = true;
        yield return new WaitForSeconds(0.12f);
        light.enabled = false;
    }
}
