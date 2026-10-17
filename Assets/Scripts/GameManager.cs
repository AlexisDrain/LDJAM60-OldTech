using UnityEngine;

public class GameManager : MonoBehaviour {
    public static bool gameIsPaused = false;
    public static Transform playerTrans;
    public static InputSystem_Actions playerActionsControls;
    void Awake()
    {

        // Time.timeScale = 0f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerActionsControls = new InputSystem_Actions();
        playerActionsControls.Enable();
        playerTrans = GameObject.Find("Player").transform;
    }

    void FixedUpdate() {

    }
}
