
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    /* Alexis Clay Drain */
    public void GetZeroGravity() {
        GameManager.playerInv.playerHasWeapon[1] = true;
    }
    public void SpawnPlayer(PlayerCheckpoint newCheckpoint) {
        GameManager.TeleportPlayer(newCheckpoint);
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
