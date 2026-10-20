using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class GameManager : MonoBehaviour {

    public static float mouseSensitivity = 1f;
    public static float keyboardSensitivity = 1f;

    public static bool playerIsAlive = true;
    public static bool startedGameOnce = false;
    public static bool gameIsPaused = true;
    public static bool gameIsEnded = false;
    public static UnityEvent playerRevive;
    public static LayerMask layerMask_Trigger;
    public static EventSystem eventSystem;
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
    public static GameObject tutorial_Controller;
    public static GameObject tutorial_Respawn;

    public static Image bg_firstLaunch;
    public static GameObject button_StartGame;
    public static GameObject button_Resume;
    public static GameObject button_RestartLevel;
    public static GameObject button_CreditsBack;
    public static GameObject creditsMenu;
    public static Canvas cheatMenuCanvas;
    public static Canvas inGameCanvas;
    public static Canvas mainMenuCanvas;
    public static Image bossHealthFill;
    public static GameObject bossHealth;
    public static EndingCutscene endingCutscene;

    public int currentLevelIdx = 0;
    public List<GameObject> levelList;
    [Header("Read only")]
    public GameObject _currentLevel;
    public PlayerCheckpoint _currentPlayerSpawn;

    void Awake()
    {

        Time.timeScale = 0f;
        mouseSensitivity = 1f;
        keyboardSensitivity = 1f;
        playerIsAlive = true;
        startedGameOnce = false;
        gameIsPaused = true;
        gameIsEnded = false;

        playerActionsControls = new InputSystem_Actions();
        playerActionsControls.Enable();
        
        layerMask_Trigger = LayerMask.NameToLayer("Trigger");
        eventSystem = transform.Find("EventSystem").GetComponent<EventSystem>();
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
        tutorial_Controller = GameObject.Find("Tutorial_Controller");
        tutorial_Controller.SetActive(false);
        tutorial_Respawn = GameObject.Find("Tutorial_Respawn");
        tutorial_Respawn.SetActive(false);

        bg_firstLaunch = GameObject.Find("Canvas/MainMenu/BG_FirstLaunch").GetComponent<Image>();
        button_StartGame = GameObject.Find("Canvas/MainMenu/StartGame");
        button_Resume = GameObject.Find("Canvas/MainMenu/ResumeGame");
        button_Resume.SetActive(false);
        button_RestartLevel = GameObject.Find("Canvas/MainMenu/RestartLevel");
        button_RestartLevel.SetActive(false);
        button_CreditsBack = GameObject.Find("Canvas/CreditsMenu/Button_CreditsBack");

        GameObject.Find("Canvas/CreditsMenu/GameVersion").GetComponent<TextMeshProUGUI>().text = $"Version: {Application.version.ToString()}";
        creditsMenu = GameObject.Find("Canvas/CreditsMenu");
        creditsMenu.SetActive(false);
        cheatMenuCanvas = GameObject.Find("Canvas/MainMenu/CheatMenu").GetComponent<Canvas>();
        cheatMenuCanvas.gameObject.SetActive(false);
        inGameCanvas = GameObject.Find("Canvas/InGame").GetComponent<Canvas>();
        inGameCanvas.enabled = false;
        mainMenuCanvas = GameObject.Find("Canvas/MainMenu").GetComponent<Canvas>();
        bossHealthFill = GameObject.Find("Canvas/InGame/BossHealth/BossHealthFill").GetComponent<Image>();
        bossHealth = GameObject.Find("Canvas/InGame/BossHealth");
        bossHealth.SetActive(false);
        endingCutscene = GameObject.Find("Canvas/Ending").GetComponent<EndingCutscene>();
        endingCutscene.gameObject.SetActive(false);

        GameObject devLevels = GameObject.Find("DevLevels");
        if(devLevels) {
            Destroy(devLevels);
        }
        Cursor.visible = false;
        // button_StartGame.GetComponent<Selectable>().Select();

        // StartLevel(0);
        // GameManager.playerTrans.position = playerSpawn.position;
    }
    private void Update() {
        if (GameManager.playerIsAlive == false && GameManager.gameIsPaused == false) {
            if (GameManager.playerActionsControls.Player.Respawn.WasPressedThisFrame()) {
            //|| (GameManager.canRespawnWithShoot <= 0f && GameManager.playerActionsControls.Player.Shoot.WasPressedThisFrame())) {
                RespawnPlayer();
            }
        }

        if(GameManager.playerActionsControls.Player.Pause.WasPressedThisFrame()) {
            if(GameManager.creditsMenu.activeSelf == true) {
                GameManager.ToggleCreditsMenu(false);
                return;
            }

            if (GameManager.startedGameOnce == false || GameManager.gameIsEnded == true) {
                return;
            }

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
        // error: Invalid worldAABB. Object is too large or too far away from the origin.
        // GameManager.pool_EnemyBullets.DeactivateAllMembers();
        // GameManager.pool_PlayerBullets.DeactivateAllMembers();

        for (var i = 2; i < 5; i++) {
            if (GameManager.playerInv.playerHasWeapon[i]) {
                GameManager.playerInv.guns[i].GetComponent<PlayerShoot>().ResetAmmo();
            }

        }
    }
    public static void ToggleCreditsMenu(bool newState) {
        if(newState == true) {
            creditsMenu.SetActive(true);
            mainMenuCanvas.gameObject.SetActive(false);
            button_CreditsBack.GetComponent<Selectable>().Select();
        } else {
            creditsMenu.SetActive(false);
            mainMenuCanvas.gameObject.SetActive(true);
            if(startedGameOnce) {
                button_Resume.GetComponent<Selectable>().Select();
            } else {
                button_StartGame.GetComponent<Selectable>().Select();
            }
        }
    }
    public static void BeatFinalBoss() {
        GameManager.gameIsEnded = true;
        GameManager.playerInv.StripGravityGun();
        GameManager.playerInv.StripWeaponsExceptGravity();
        bg_firstLaunch.gameObject.SetActive(true);
        button_Resume.SetActive(false);
        button_StartGame.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;

        endingCutscene.InvokeEnding();
    }
    public static void RestartGameScene() {
        playerActionsControls.Disable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame() {
        GameManager.gameIsPaused = false;
        GameManager.startedGameOnce = true;
        bg_firstLaunch.gameObject.SetActive(false);
        button_StartGame.SetActive(false);
        button_RestartLevel.SetActive(true);
        button_Resume.SetActive(true);
        StartLevel(0);
        Time.timeScale = 1f;
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGameCanvas.enabled = true;
        mainMenuCanvas.gameObject.SetActive(false);
    }
    public void ResumeGame() {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        // Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inGameCanvas.enabled = true;
        mainMenuCanvas.gameObject.SetActive(false);
    }
    public void PauseGame() {
        GameManager.gameIsPaused = true;
        button_Resume.GetComponent<Selectable>().Select();

        Time.timeScale = 0f;
        // Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        inGameCanvas.enabled = false;
        mainMenuCanvas.gameObject.SetActive(true);
    }
    public static void ApplyOptions() {
        print("hello");
    }
    public static void RestartLevel() {
        StartLevel(GameManager.myGameManager.currentLevelIdx);
    }
    public static void StartLevel(int levelIdx) {
        GameManager.myGameManager.currentLevelIdx = levelIdx;
        TeleportPlayer(GameManager.myGameManager.levelList[levelIdx]);
    }
    public static void TeleportPlayer(GameObject newLevel) {
        Destroy(GameManager.myGameManager._currentLevel);
        GameManager.myGameManager._currentLevel = GameObject.Instantiate(newLevel);

        // reset stats
        
        GameManager.bossHealth.SetActive(false);
        GameManager.playerTrans.GetComponent<PlayerHealth>().health = 100;
        GameManager.playerInv.guns[1].GetComponent<PlayerZeroGrav>().RemoveAttachedObject();

        PlayerCheckpoint newCheckpoint = newLevel.GetComponent<LevelProperties>().myCheckpoint;

        GameManager.myGameManager._currentPlayerSpawn = newCheckpoint;

        if(newLevel.GetComponent<LevelProperties>().stripWeaponsExceptGravity) {
            GameManager.playerInv.StripWeaponsExceptGravity();
        }
        if (newLevel.GetComponent<LevelProperties>().stripGravity) {
            GameManager.playerInv.StripGravityGun();
        }

        if (newCheckpoint.darkLevel == true) {
            mainCamLight.gameObject.SetActive(false);
            mainCamDark.gameObject.SetActive(true);
        } else {
            mainCamLight.gameObject.SetActive(true);
            mainCamDark.gameObject.SetActive(false);
        }

        if(newCheckpoint.showMoveTutorial) {
            tutorial_Move.SetActive(true);
            tutorial_Controller.SetActive(true);
        } else {
            tutorial_Move.SetActive(false); // not needed. just in case.
            tutorial_Controller.SetActive(false);
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
