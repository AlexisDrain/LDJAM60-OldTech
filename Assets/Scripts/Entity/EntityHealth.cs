using System.Collections.Generic;
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
    public List<AudioClip> hurtSFX = new List<AudioClip>();
    public List<AudioClip> deathSFX = new List<AudioClip>();
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
            int randomIdx = Random.Range(0, deathSFX.Count);
            if(deathSFX.Count >= 1 && deathSFX[randomIdx]) {
                GameManager.soundManager.SpawnLoudAudio(deathSFX[randomIdx]);
            }

            gameObject.SetActive(false);
        } else {
            int randomIdx = Random.Range(0, hurtSFX.Count);
            if(hurtSFX.Count >= 1 && hurtSFX[randomIdx]) {
                GetComponent<AudioSource>().clip = hurtSFX[randomIdx];
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
