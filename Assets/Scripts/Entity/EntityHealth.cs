using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    /* Alexis Clay Drain */
    private int _health;
    public int maxHealth;
    public bool bleed = false;
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
        health -= damage;
        if(bleed == true) {
            GameManager.particle_blood.transform.position = transform.position;
            GameManager.particle_blood.Play();
        }
        if (health <= 0) {
            if(spawnOnDeath) {
                GameObject spawn = GameObject.Instantiate(spawnOnDeath);
                if(spawn.GetComponent<Rigidbody>()) {
                    spawn.GetComponent<Rigidbody>().MovePosition(transform.position);
                }
                spawn.transform.position = transform.position;
            }
            gameObject.SetActive(false);
        }
    }
}
