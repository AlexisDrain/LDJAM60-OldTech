using UnityEngine;
using UnityEngine.Events;

public class BatterySlot : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onBatteryInsert;
    public AudioClip batteryClip;
    public GameObject fakeBattery;
    void Start() {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Moveable>() && other.GetComponent<Moveable>().battery == true) {
            other.gameObject.SetActive(false);
            fakeBattery.SetActive(true);
            GameManager.soundManager.SpawnLoudAudio(batteryClip);
            onBatteryInsert.Invoke();
        }
    }
}
