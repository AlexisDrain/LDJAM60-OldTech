using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    /* Alexis Clay Drain */

    private int _health;
    public int maxHealth;

    public int health {
        get {
            return _health;
        }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    public void OnEnable() {
        health = maxHealth;
    }
    public void DoDamage(int damage) {
        if (damage < 0) {
            damage = -damage;
            print("Damage shouldn't be negative");
        }
        health -= damage;
        if (health <= 0) {
            GameManager.playerIsAlive = false;
            // gameObject.SetActive(false);
        }
    }
}
