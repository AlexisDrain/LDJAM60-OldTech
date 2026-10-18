using UnityEngine;

public enum Item {
    None,
    Revolver,
    MachineGun,
    ZeroGravity,
    Health
}

public class ItemPickup : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Item myItem;
    public AudioClip pickupItem;
    public void TryPickup() {
        if (myItem == Item.ZeroGravity) {
            if(GameManager.playerInv.playerHasWeapon[1] == false) {
                GameManager.playerInv.playerHasWeapon[1] = true;
                GameManager.playerInv.SwitchToWeapon(1);
            }
        } else if (myItem == Item.Revolver) {
            if (GameManager.playerInv.playerHasWeapon[2] == false) {
                GameManager.playerInv.playerHasWeapon[2] = true;
                GameManager.playerInv.SwitchToWeapon(2);
            }
            GameManager.playerInv.guns[2].GetComponent<PlayerShoot>().ammo += 20;
        } else if (myItem == Item.MachineGun) {
            if (GameManager.playerInv.playerHasWeapon[3] == false) {
                GameManager.playerInv.playerHasWeapon[3] = true;
                GameManager.playerInv.SwitchToWeapon(3);
            }
            GameManager.playerInv.guns[3].GetComponent<PlayerShoot>().ammo += 20;
        } else if (myItem == Item.Health) {
            GameManager.playerTrans.GetComponent<PlayerHealth>().DoHealth(100);
        }
        GameManager.soundManager.SpawnLoudAudio(pickupItem);
        gameObject.SetActive(false);
        GameManager.playerInv.UpdateWeapons();
    }
}
