using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayAudioSourceSFX: MonoBehaviour {
    public AudioSource overrideAudioSource; // useful for animations like doors in The Vault
	public float overrideAudioSourceVolume = 1f;
	public Vector2 overridePitchRange = Vector2.one;
    public List<AudioClip> randomSFX;
    //private Transform myTransform;
	public void PlayRandomSFXFromList() {
        int randClip = Random.Range(0, randomSFX.Count);
        float randPitch = Random.Range(overridePitchRange.x, overridePitchRange.y);

        AudioSource source;
        if (overrideAudioSource != null) {
            source = overrideAudioSource;
        } else {
            source = GetComponent<AudioSource>();
        }

        source.clip = randomSFX[randClip];
        source.pitch = randPitch;

        source.Play();
    }
    public void PlayRandomSFXFromListInGameManager() {
        int randClip = Random.Range(0, randomSFX.Count);
        float randPitch = Random.Range(overridePitchRange.x, overridePitchRange.y);
        GameManager.soundManager.SpawnLoudAudio(randomSFX[randClip], overridePitchRange, overrideAudioSourceVolume);
    }
    public void PlayAudioSource (AudioClip newAudioClip) {
        float randPitch = Random.Range(overridePitchRange.x, overridePitchRange.y);

        AudioSource source;
        if (overrideAudioSource != null) {
            source = overrideAudioSource;
        } else {
            source = GetComponent<AudioSource>();
        }

        source.pitch = randPitch;

        source.clip = newAudioClip;
        source.volume = overrideAudioSourceVolume;
        source.Play();
		//myTransform = transform;
	}
	public void PlayLoudSFXInGameManager(AudioClip newAudioClip) {

		GameManager.soundManager.SpawnLoudAudio(newAudioClip, overridePitchRange, overrideAudioSourceVolume);
	}


	/*
	private void Update () {
		
	}
	*/
}
