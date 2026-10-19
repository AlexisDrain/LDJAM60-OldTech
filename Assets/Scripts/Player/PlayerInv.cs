using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Weapon {
    None,
    zeroGravity,
    Pistol,
    MachineGun,
    EnemyBullet,
    Shotgun
}
public class PlayerInv : MonoBehaviour
{
    /* Alexis Clay Drain */
    [Header("Read only")]
    public int currentSlot = 0;
    [Header("Setup once")]
    public Image crosshairImage;
    public Image crosshairImagePhantom;
    public Image infinitySign;
    public AudioClip switchWeaponSFX;

    public List<bool> playerHasWeapon = new List<bool>();
    public List<Image> gunBGs = new List<Image>();
    public List<TextMeshProUGUI> gunNumbers = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> gunAmmoNumber= new List<TextMeshProUGUI>();
    public List<Sprite> crosshairs = new List<Sprite>();
    public List<GameObject> gunsCanvas = new List<GameObject>();
    public List<GameObject> guns = new List<GameObject>();
    void Start() {
        foreach (GameObject gun in guns) {
            gun.SetActive(false);
        }
        foreach (GameObject gunCan in gunsCanvas) {
            if(gunCan) {
                gunCan.SetActive(false);
            }
        }
        // starting game with no weapons
        guns[0].SetActive(true);
        crosshairImage.sprite = crosshairs[0];
        crosshairImagePhantom.sprite = crosshairs[0];
    }
    public void UpdateWeapons() {
        for (var i = 1; i < playerHasWeapon.Count; i++) {
            if (playerHasWeapon[i] == true && gunsCanvas[i] == true) {
                gunsCanvas[i].SetActive(true);
            } else {
                gunsCanvas[i].SetActive(false);
            }
        }
    }
    public void StripGravityGun() {

        playerHasWeapon[1] = false;
        guns[1].SetActive(false);
        UpdateWeapons();
        SwitchToWeapon(1);
    }
    public void StripWeaponsExceptGravity() {

        for (var i = 2; i < playerHasWeapon.Count; i++) {
            playerHasWeapon[i] = false;
            guns[i].GetComponent<PlayerShoot>().ResetAmmo();
            guns[i].SetActive(false);
        }
        UpdateWeapons();
        SwitchToWeapon(1);
    }
    public void DisableCurrentWeapon() {
        // pass
    }
    public void SwitchToWeapon(int newSlot) {
        if (playerHasWeapon[newSlot] == false) {
            return;
        }
        currentSlot = newSlot;

        if(newSlot != 0) {
            GameManager.soundManager.SpawnLoudAudio(switchWeaponSFX);
        }

        foreach(GameObject gun in guns) {
            gun.SetActive(false);
        }
        guns[newSlot].SetActive(true);

        foreach (Image gunBG in gunBGs) {
            if(gunBG) {
                gunBG.color = Color.white;
            }
        }
        gunBGs[newSlot].color = new Color(0f, 0.66f, 1f);

        foreach (TextMeshProUGUI gunNumber in gunNumbers) {
            if(gunNumber) {
                gunNumber.color = Color.white;
            }
        }
        gunNumbers[newSlot].color = new Color(0f, 0.66f, 1f);

        foreach (TextMeshProUGUI gunAmmoNumber in gunAmmoNumber) {
            if (gunAmmoNumber) {
                gunAmmoNumber.color = Color.white;
            }
        }
        gunAmmoNumber[newSlot].color = new Color(0f, 0.66f, 1f);

        crosshairImage.sprite = crosshairs[newSlot];
        crosshairImagePhantom.sprite = crosshairs[newSlot];

        if (newSlot == 1) {
            crosshairImagePhantom.rectTransform.anchoredPosition = new Vector2(0f, 0f);
            infinitySign.color = new Color(0f, 0.66f, 1f);
        } else {
            crosshairImagePhantom.rectTransform.anchoredPosition = new Vector2(120f, 0f);
            infinitySign.color = Color.white;
        }
    }
    private void Update() {
        if(GameManager.playerActionsControls.Player.Weapon1.WasPressedThisFrame()) {
            SwitchToWeapon(1);
        }
        if (GameManager.playerActionsControls.Player.Weapon2.WasPressedThisFrame()) {
            SwitchToWeapon(2);
        }
        if (GameManager.playerActionsControls.Player.Weapon3.WasPressedThisFrame()) {
            SwitchToWeapon(3);
        }
        if (GameManager.playerActionsControls.Player.Weapon4.WasPressedThisFrame()) {
            SwitchToWeapon(4);
        }
    }
}
