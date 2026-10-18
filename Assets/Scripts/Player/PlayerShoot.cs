using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    /* Alexis Clay Drain */
    private int _ammo = 99;
    public int ammo {
        get {
            return _ammo;
        }
        set {
            _ammo = Mathf.Clamp(value, 0, 99);
            ammoCounter.text = _ammo.ToString();
        }
    }
    public Weapon myWeapon;
    public TextMeshProUGUI ammoCounter;
    public AudioClip emptySFX;
    public AudioClip shootSFX;

    public float defaultNextShotTime;
    private float currentNextShotTime;
    private Light light;
    void Start() {
        light = GetComponent<Light>();
    }
    private void FixedUpdate() {
        if (currentNextShotTime > 0f) {
            currentNextShotTime -= Time.deltaTime;
        }
    }
    private void Update() {
        if (myWeapon == Weapon.MachineGun) {

            if (GameManager.playerActionsControls.Player.Shoot.IsPressed() && currentNextShotTime <= 0f) {
                currentNextShotTime = defaultNextShotTime;

                // shoot()
                if (ammo <= 0) {
                    GameManager.soundManager.SpawnLoudAudio(emptySFX);
                    return;
                }
                GameManager.soundManager.SpawnLoudAudio(shootSFX);
                GameManager.playerTrans.GetComponent<Rigidbody>().AddForce(-GameManager.playerTrans.forward * 1f, ForceMode.Impulse);
                // StartCoroutine(ShootCoroutine());
                GameObject bullet = GameManager.pool_PlayerBullets.Spawn(transform.position);
                bullet.GetComponent<BulletController>().direction = GameManager.playerTrans.forward;

                ammo -= 1;
                if (ammo <= 0) {
                    GameManager.playerInv.DisableCurrentWeapon();
                }
            }
        }
        if(myWeapon == Weapon.Pistol) {
            if(GameManager.playerActionsControls.Player.Shoot.WasPressedThisFrame()) {
                // shoot()
                if (ammo <= 0) {
                    GameManager.soundManager.SpawnLoudAudio(emptySFX);
                    return;
                }
                GameManager.soundManager.SpawnLoudAudio(shootSFX);
                GameManager.playerTrans.GetComponent<Rigidbody>().AddForce(-GameManager.playerTrans.forward * 2f, ForceMode.Impulse);
                StartCoroutine(ShootCoroutine());
                GameObject bullet = GameManager.pool_PlayerBullets.Spawn(transform.position);
                bullet.GetComponent<BulletController>().direction = GameManager.playerTrans.forward;

                ammo -= 1;
                if (ammo <= 0) {
                    GameManager.playerInv.DisableCurrentWeapon();
                }
            }

        }
    }
    private IEnumerator ShootCoroutine() {
        light.enabled = true;
        yield return new WaitForSeconds(0.12f);
        light.enabled = false;
    }
}
