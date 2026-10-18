using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    /* Alexis Clay Drain */

    private int _health;
    public int maxHealth;
    private float canGetDamaged = 0f;
    public int health {
        get {
            return _health;
        }
        set {
            _health = Mathf.Clamp(value, 0, maxHealth);
            UpdateHealthBar();
        }
    }
    public List<Image> healthBars = new List<Image>();
    public Sprite sprite_healthFull;
    public Sprite sprite_healthEmpty;

    public AudioClip hurtClip;
    public AudioClip healClip;
    public AudioClip deathClip;
    private void OnEnable() {
        health = maxHealth;
    }
    private void FixedUpdate() {
        if (canGetDamaged >= 0f) {
            canGetDamaged -= Time.deltaTime;
        }
    }
    private void UpdateHealthBar() {
        foreach (Image healthBar in healthBars) {
            healthBar.sprite = sprite_healthEmpty;
        }
        if (health >= 20) {
            healthBars[0].sprite = sprite_healthFull;
        }
        if (health >= 40) {
            healthBars[1].sprite = sprite_healthFull;
        }
        if (health >= 60) {
            healthBars[2].sprite = sprite_healthFull;
        }
        if (health >= 80) {
            healthBars[3].sprite = sprite_healthFull;
        }
        if (health >= 100) {
            healthBars[4].sprite = sprite_healthFull;
        }
    }
    public void DoHealth(int extraHealth) {
        if (extraHealth < 0) {
            extraHealth = -extraHealth;
            print("extraHealth shouldn't be negative");
        }
        health += extraHealth;
        GameManager.soundManager.SpawnLoudAudio(healClip);
    }
    public void DoDamage(int damage) {
        if(canGetDamaged > 0f) {
            return;
        }
        canGetDamaged = 1f;

        if (damage < 0) {
            damage = -damage;
            print("Damage shouldn't be negative");
        }
        health -= damage;

        GameManager.particle_blood.transform.position = transform.position;
        GameManager.particle_blood.Play();

        if (health <= 0) {
            GameManager.playerIsAlive = false;
            // gameObject.SetActive(false);
            GameManager.soundManager.SpawnLoudAudio(deathClip);
        } else {
            GameManager.soundManager.SpawnLoudAudio(hurtClip);
        }
    }
}
