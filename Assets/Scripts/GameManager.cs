using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool playerIsAlive = true;
    public static bool gameIsPaused = false;
    public static InputSystem_Actions playerActionsControls;

    public static GameManager myGameManager;
    public static SoundManager soundManager;
    public static Transform playerTrans;
    public static PlayerInv playerInv;
    public static Camera mainCamLight;
    public static Pool pool_PlayerBullets;
    public static Pool pool_EnemyBullets;
    public static ParticleSystem particle_blood;
    
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
        playerInv = GameObject.Find("PlayerDolly").GetComponent<PlayerInv>();
        playerTrans = GameObject.Find("Player").transform;
        mainCamLight = GameObject.Find("MainCameraLight").GetComponent<Camera>();
        pool_PlayerBullets = transform.Find("Pool_PlayerBullets").GetComponent<Pool>();
        pool_EnemyBullets = transform.Find("Pool_EnemyBullets").GetComponent<Pool>();
        particle_blood = transform.Find("Blood").GetComponent<ParticleSystem>();

        GameManager.playerTrans.GetComponent<Rigidbody>().Move(playerSpawn.position, playerSpawn.rotation);
        // GameManager.playerTrans.position = playerSpawn.position;
    }
}
