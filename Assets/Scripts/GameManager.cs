using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool playerIsAlive = true;
    public static bool startedGameOnce = false;
    public static bool gameIsPaused = true;
    public static InputSystem_Actions playerActionsControls;

    public static GameManager myGameManager;
    public static SoundManager soundManager;
    public static Transform playerTrans;
    public static PlayerInv playerInv;
    public static TrailRenderer playerTrail;
    public static Camera mainCamLight;
    public static Camera mainCamDark;
    public static Pool pool_PlayerBullets;
    public static Pool pool_EnemyBullets;
    public static ParticleSystem particle_blood;
    public static GameObject tutorial_GrabObject;
    public static GameObject tutorial_Move;

    public static Canvas cheatMenuCanvas;
    public static Canvas inGameCanvas;
    public static Canvas mainMenuCanvas;

    public PlayerCheckpoint playerSpawn;

    void Awake()
    {

        Time.timeScale = 0f;

        playerActionsControls = new InputSystem_Actions();
        playerActionsControls.Enable();

        myGameManager = GetComponent<GameManager>();
        soundManager = GetComponent<SoundManager>();
        soundManager.Init();
        playerInv = GameObject.Find("PlayerDolly").GetComponent<PlayerInv>();
        playerTrail = GameObject.Find("PlayerTrail").GetComponent<TrailRenderer>();
        playerTrans = GameObject.Find("Player").transform;
        mainCamLight = GameObject.Find("MainCameraLight").GetComponent<Camera>();
        mainCamDark = GameObject.Find("MainCameraDark").GetComponent<Camera>();
        mainCamDark.gameObject.SetActive(false);
        pool_PlayerBullets = transform.Find("Pool_PlayerBullets").GetComponent<Pool>();
        pool_EnemyBullets = transform.Find("Pool_EnemyBullets").GetComponent<Pool>();
        particle_blood = transform.Find("Blood").GetComponent<ParticleSystem>();
        tutorial_GrabObject = GameObject.Find("Tutorial_GrabObject");
        tutorial_Move = GameObject.Find("Tutorial_Move");
        tutorial_Move.SetActive(false);

        cheatMenuCanvas = GameObject.Find("Canvas/MainMenu/CheatMenu").GetComponent<Canvas>();
        cheatMenuCanvas.enabled = false;
        inGameCanvas = GameObject.Find("Canvas/InGame").GetComponent<Canvas>();
        inGameCanvas.enabled = false;
        mainMenuCanvas = GameObject.Find("Canvas/MainMenu").GetComponent<Canvas>();

        TeleportPlayer(playerSpawn);
        // GameManager.playerTrans.position = playerSpawn.position;
    }
    private void Update() {
        if(GameManager.startedGameOnce == false) {
            return;
        }
        if(GameManager.playerActionsControls.Player.Pause.WasPressedThisFrame()) {
            if(GameManager.gameIsPaused == false) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }
    public void StartGame() {
        GameManager.gameIsPaused = false;
        GameManager.startedGameOnce = true;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGameCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }
    public void ResumeGame() {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGameCanvas.enabled = true;
        mainMenuCanvas.enabled = false;
    }
    public void PauseGame() {
        GameManager.gameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inGameCanvas.enabled = false;
        mainMenuCanvas.enabled = true;
    }
    public static void TeleportPlayer(PlayerCheckpoint newCheckpoint) {
        if(newCheckpoint.darkLevel == true) {
            mainCamLight.gameObject.SetActive(false);
            mainCamDark.gameObject.SetActive(true);
        } else {
            mainCamLight.gameObject.SetActive(true);
            mainCamDark.gameObject.SetActive(false);
        }

        if(newCheckpoint.showMoveTutorial) {
            tutorial_Move.SetActive(true);
        } else {
            tutorial_Move.SetActive(false); // not needed. just in case.
        }

        

        GameManager.playerTrans.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GameManager.playerTrans.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        Vector3 newPos = new Vector3(newCheckpoint.transform.position.x, newCheckpoint.transform.position.y, 0f);
        if (newCheckpoint.spawnPlayerInThisRotation == true) {
            GameManager.playerTrans.GetComponent<Rigidbody>().Move(newPos, newCheckpoint.transform.rotation);
        } else {
            GameManager.playerTrans.GetComponent<Rigidbody>().MovePosition(newPos);
        }
        GameManager.myGameManager.StartCoroutine(GameManager.myGameManager.TeleportWait());
    }
    private IEnumerator TeleportWait() {
        GameManager.playerTrail.enabled = false;
        GameManager.playerTrail.Clear();
        yield return new WaitForSeconds(0.5f);
        GameManager.playerTrail.Clear();
        GameManager.playerTrail.enabled = true;
    }
}
