using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameIsPaused = false;
    public static InputSystem_Actions playerActionsControls;

    public static GameManager myGameManager;
    public static SoundManager soundManager;
    public static Transform playerTrans;
    public static Camera mainCamLight;

    public Transform playerSpawn;

    void Awake()
    {

        // Time.timeScale = 0f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerActionsControls = new InputSystem_Actions();
        playerActionsControls.Enable();

        myGameManager = GetComponent<GameManager>();
        soundManager = GetComponent<SoundManager>();
        soundManager.Init();
        playerTrans = GameObject.Find("Player").transform;
        mainCamLight = GameObject.Find("MainCameraLight").GetComponent<Camera>();

        GameManager.playerTrans.GetComponent<Rigidbody>().MovePosition(playerSpawn.position);
        // GameManager.playerTrans.position = playerSpawn.position;
    }
}
