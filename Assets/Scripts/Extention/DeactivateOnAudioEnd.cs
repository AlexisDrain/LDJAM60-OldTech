using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnAudioEnd : MonoBehaviour {

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (!audioSource.isPlaying) {
            EndAudio();
        }
    }

    private void EndAudio() {
        gameObject.SetActive(false);
    }
}
