using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCrosshair : MonoBehaviour
{
    /* Alexis Clay Drain */
    
    void LateUpdate() {
        transform.position = GameManager.playerActionsControls.Player.MousePosition.ReadValue<Vector2>();
    }
}
