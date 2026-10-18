using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Weapon {
    None,
    zeroGravity,
    Pistol,
    MachineGun,
    EnemyBullet
}
public class PlayerInv : MonoBehaviour
{
    /* Alexis Clay Drain */
    [Header("Read only")]
    public int currentSlot = 0;
    [Header("Setup once")]
    public Image crosshairImage;
    public Image crosshairImagePhantom;

    public List<bool> playerHasWeapon = new List<bool>();
    public List<Image> gunBGs = new List<Image>();
    public List<TextMeshProUGUI> gunNumbers = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> gunAmmoNumber= new List<TextMeshProUGUI>();
    public List<Sprite> crosshairs = new List<Sprite>();
    public List<GameObject> guns = new List<GameObject>();
    void Start() {
        foreach (GameObject gun in guns) {
            gun.SetActive(false);
        }
        // starting game with no weapons
        guns[0].SetActive(true);
        crosshairImage.sprite = crosshairs[0];
        crosshairImagePhantom.sprite = crosshairs[0];
    }
    public void DisableCurrentWeapon() {
        // pass
    }
    public void SwitchToWeapon(int newSlot) {
        if (playerHasWeapon[newSlot] == false) {
            return;
        }
        currentSlot = newSlot;

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
        } else {
            crosshairImagePhantom.rectTransform.anchoredPosition = new Vector2(120f, 0f);
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
    }
}
