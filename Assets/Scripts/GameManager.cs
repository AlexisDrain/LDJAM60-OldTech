using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static bool playerIsAlive = true;
    public static bool startedGameOnce = false;
    public static bool gameIsPaused = true;
    public static UnityEvent playerRevive;
    public static LayerMask layerMask_Trigger;
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
    public static GameObject tutorial_Respawn;

    public static Canvas cheatMenuCanvas;
    public static Canvas inGameCanvas;
    public static Canvas mainMenuCanvas;

    public int currentLevelIdx = 0;
    public List<GameObject> levelList;
    [Header("Read only")]
    public GameObject _currentLevel;
    public PlayerCheckpoint _currentPlayerSpawn;

    void Awake()
    {

        Time.timeScale = 0f;

        playerActionsControls = new InputSystem_Actions();
        playerActionsControls.Enable();

        layerMask_Trigger = LayerMask.NameToLayer("Trigger");

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
        tutorial_Respawn = GameObject.Find("Tutorial_Respawn");
        tutorial_Respawn.SetActive(false);

        cheatMenuCanvas = GameObject.Find("Canvas/MainMenu/CheatMenu").GetComponent<Canvas>();
        cheatMenuCanvas.enabled = false;
        inGameCanvas = GameObject.Find("Canvas/InGame").GetComponent<Canvas>();
        inGameCanvas.enabled = false;
        mainMenuCanvas = GameObject.Find("Canvas/MainMenu").GetComponent<Canvas>();

        GameObject devLevels = GameObject.Find("DevLevels");
        if(devLevels) {
            Destroy(devLevels);
        }

        StartLevel(0);
        // GameManager.playerTrans.position = playerSpawn.position;
    }
    private void Update() {
        if(GameManager.startedGameOnce == false) {
            return;
        }
        if (GameManager.playerIsAlive == false) {
            if (GameManager.playerActionsControls.Player.Respawn.WasPressedThisFrame()) {
            //|| (GameManager.canRespawnWithShoot <= 0f && GameManager.playerActionsControls.Player.Shoot.WasPressedThisFrame())) {
                RespawnPlayer();
            }
        }

        if(GameManager.playerActionsControls.Player.Pause.WasPressedThisFrame()) {
            if(GameManager.gameIsPaused == false) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }
    /*
    private static float canRespawnWithShoot = 0f;
    private void FixedUpdate() {
        if(canRespawnWithShoot > 0f) {
            canRespawnWithShoot -= Time.deltaTime;
        }
    }
    */
    public static void KillPlayer() {
        // canRespawnWithShoot = 1f;
        GameManager.playerIsAlive = false;
        tutorial_Respawn.SetActive (true);
    }
    private void RespawnPlayer() {
        GameManager.playerIsAlive = true;
        GameManager.playerTrans.GetComponent<PlayerHealth>().health = 100;
        tutorial_Respawn.SetActive(false);
        GameManager.StartLevel(currentLevelIdx);
        GameManager.particle_blood.Clear();

        for (var i = 2; i < 5; i++) {
            if (GameManager.playerInv.playerHasWeapon[i]) {
                GameManager.playerInv.guns[i].GetComponent<PlayerShoot>().ResetAmmo();
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
        mainMenuCanvas.gameObject.SetActive(false);
    }
    public void ResumeGame() {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGameCanvas.enabled = true;
        mainMenuCanvas.gameObject.SetActive(false);
    }
    public void PauseGame() {
        GameManager.gameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inGameCanvas.enabled = false;
        mainMenuCanvas.gameObject.SetActive(true);
    }
    public static void StartLevel(int levelIdx) {
        GameManager.myGameManager.currentLevelIdx = levelIdx;
        TeleportPlayer(GameManager.myGameManager.levelList[levelIdx]);
    }
    public static void TeleportPlayer(GameObject newLevel) {
        Destroy(GameManager.myGameManager._currentLevel);
        GameManager.myGameManager._currentLevel = GameObject.Instantiate(newLevel);

        PlayerCheckpoint newCheckpoint = newLevel.GetComponent<LevelProperties>().myCheckpoint;

        GameManager.myGameManager._currentPlayerSpawn = newCheckpoint;

        if (newCheckpoint.darkLevel == true) {
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
