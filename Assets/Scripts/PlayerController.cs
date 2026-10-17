using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float cameraDistanceFromPlayer = 12f;
    public float moveForce = 10f;
    public float maxSpeed = 15f;
    public float torqueForce = 1f;
    public Rigidbody playerRigidbody;
    public Transform playerGraphics;
    public Transform playerHand;
    public RectTransform crosshairUI;
    public LineRenderer myLine;
    public Transform crosshair;

    private Vector2 pointerDelta;
    void Start()
    {

    }
    private void LateUpdate() {
        // playerGraphics.position = Vector3.Lerp(playerGraphics.position, playerRigidbody.position, 30f * Time.deltaTime);
        playerGraphics.position = playerRigidbody.transform.position;
        playerGraphics.rotation = Quaternion.Lerp(playerGraphics.rotation, playerRigidbody.rotation * Quaternion.Euler(0f, -90f, 0f), 10f * Time.deltaTime);
        myLine.SetPosition(0, new Vector3(playerHand.position.x, playerHand.position.y, 0f));
        myLine.SetPosition(1, new Vector3(crosshair.position.x, crosshair.position.y, 0f));
    }
    private void FixedUpdate() {
        //// mouse
        /*
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = cameraDistanceFromPlayer;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        //Vector3 ViewportPos = Camera.main.WorldToScreenPoint(worldPoint);

        crosshair.position = worldPoint; // in world position of crosshair. Make calculations easier
        crosshairUI.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, crosshair.transform.TransformPoint(Vector3.zero));

        Vector2 direction = (crosshair.position - GameManager.playerTrans.position).normalized;
        float angle = Vector3.Angle(GameManager.playerTrans.forward, direction);

        if(angle > 30) {
            print("up");
            playerRigidbody.AddTorque(0f, 0f, torqueForce);
        }
        */

        if (GameManager.gameIsPaused == true) {
            return;
        }
        //if(GameManager.gameIsPaused == false && Input.GetMouseButtonDown(0)) {
        //if (Input.GetMouseButtonDown(0)) {
        if (GameManager.playerActionsControls.UI.Click.WasPressedThisFrame()) {
            Cursor.lockState = CursorLockMode.Locked;
        }

        pointerDelta = GameManager.playerActionsControls.Player.Look.ReadValue<Vector2>();

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(-pointerDelta.y, 0, 0));
        playerRigidbody.angularVelocity *= 0.95f;
        // playerRigidbody.AddTorque(new Vector3(0, 0, -pointerDelta.y));

        //// keyboard
        Vector3 inputAxis = GameManager.playerActionsControls.Player.Move.ReadValue<Vector2>();
        inputAxis.Normalize();
        if (Mathf.Abs(inputAxis.x) > 0) {
            // playerRigidbody.MovePosition(GameManager.playerTrans.position + (inputAxis * horizontalMoveForce * Time.deltaTime));
            playerRigidbody.AddForce(GameManager.playerTrans.forward * moveForce * inputAxis.x);
        }
        if (Mathf.Abs(inputAxis.y) > 0) {
            // playerRigidbody.MovePosition(GameManager.playerTrans.position + (GameManager.playerTrans.up * horizontalMoveForce * inputAxis.y * Time.deltaTime));
            playerRigidbody.AddForce(GameManager.playerTrans.up * moveForce * inputAxis.y);
        }

        // clamp speed
        playerRigidbody.linearVelocity = Vector3.ClampMagnitude(playerRigidbody.linearVelocity, maxSpeed);
    }
}
