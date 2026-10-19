using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour
{
    /* Alexis Clay Drain */
    private int _health;
    public int maxHealth;
    public bool bleed = false;
    public UnityEvent onHurt;
    public UnityEvent onKill;
    public GameObject spawnOnDeath;
    public int health {
        get {
            return _health;
        }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
        }
    }
    void Start() {
        
    }
    private void OnEnable() {
        health = maxHealth;
    }
    public void DoDamage(int damage) {
        if (damage < 0) {
            damage = -damage;
            print("Damage shouldn't be negative");
        }
        onHurt.Invoke();
        health -= damage;
        if (health <= 0) {
            onKill.Invoke();
            if (spawnOnDeath) {
                GameObject spawn = GameObject.Instantiate(spawnOnDeath, transform.position, Quaternion.identity, GameManager.myGameManager._currentLevel.transform);
                if(spawn.GetComponent<Rigidbody>()) {
                    spawn.GetComponent<Rigidbody>().MovePosition(transform.position);
                }
                spawn.transform.position = transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}
