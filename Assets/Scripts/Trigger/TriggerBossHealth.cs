using UnityEngine;

public class TriggerBossHealth : MonoBehaviour
{
    /* Alexis Clay Drain */
    private EntityHealth bossHealth;
    private float currentHealth;
    private void OnEnable() {
        bossHealth = GameObject.Find("Boss").GetComponent<EntityHealth>();
    }
    private void Update() {
        if(bossHealth == null) {
            bossHealth = GameObject.Find("Boss").GetComponent<EntityHealth>();
            return;
        }
        if (currentHealth != bossHealth.health) {
            currentHealth = bossHealth.health;
            GameManager.bossHealthFill.fillAmount = (float)bossHealth.health / (float)bossHealth.maxHealth;
        }
    }
    public void ShowBossHealth(bool newState) {
        if (newState == true) {
            GameManager.bossHealth.SetActive(true);
            GameManager.bossHealthFill.fillAmount = 1;
        } else {
            GameManager.bossHealth.SetActive(false);
        }
    }
}
