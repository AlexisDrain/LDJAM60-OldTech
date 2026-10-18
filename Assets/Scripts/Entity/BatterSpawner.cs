using UnityEngine;

public class BatterSpawner : MonoBehaviour {
    /* Alexis Clay Drain */
    public GameObject batteryPrefab;
    public Transform spawnLocation;
    private GameObject currentBattery;

    private float currentTimeToRespawn = 0f;
    void Start() {

    }
    private void FixedUpdate() {
        if(currentTimeToRespawn >= 0f) {
            currentTimeToRespawn -= Time.deltaTime;
        }
    }
    public void SpawnBattery() {
        if(currentTimeToRespawn > 0f) {
            return;
        }
        if(currentBattery) {
            Destroy(currentBattery);
        }
        currentTimeToRespawn = 4f;
        currentBattery = GameObject.Instantiate(batteryPrefab, spawnLocation.position, spawnLocation.rotation, transform);
    }
}
