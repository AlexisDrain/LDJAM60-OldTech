
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    /* Alexis Clay Drain */
    public void GetGun(int idx) {
        GameManager.playerInv.playerHasWeapon[idx] = true;
        GameManager.playerInv.UpdateWeapons();
    }
    public void SpawnPlayer(GameObject newLevel) {
        GameManager.TeleportPlayer(newLevel);
    }
    public void Update() {

        if (GameManager.playerActionsControls.Player.CheatMenu.WasPressedThisDynamicUpdate()) {
            GameManager.cheatMenuCanvas.enabled = true;
            if(GameManager.gameIsPaused == false) {
                GameManager.myGameManager.PauseGame();
            }
        }
    }
}
