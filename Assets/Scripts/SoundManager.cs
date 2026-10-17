using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static Pool pool_LoudAudioSource;

    // Tracks the exact time (Time.time) each clip was last allowed to play
    private Dictionary<AudioClip, float> clipPlayTimestamps = new();
    private float soundCooldown = 0.05f;

    public void Init()
    {
        pool_LoudAudioSource = transform.Find("pool_LoudAudioSource").GetComponent<Pool>();
    }

    public AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f, bool randomPitch = false)
    {
        if (newAudioClip == null)
        {
            return null;
        }

        float sfxPitch;
        if (pitch.x <= 0.1f)
        {
            sfxPitch = 1;
        }
        else
        {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.pitch = sfxPitch;
        audioObject.clip = newAudioClip;
        audioObject.volume = newVolume;
        audioObject.ignoreListenerPause = true; // TESTING
        PlaySoundFromAudioSource(audioObject, randomPitch: randomPitch);
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }

    public void PlaySoundFromAudioSource(AudioSource audioSource, bool randomPitch = false)
    {
        // Don't play audio clips too soon
        // This avoids sounds appearing super loud when played multiple times at the same time
        // i.e. in menu
        if (clipPlayTimestamps.TryGetValue(audioSource.clip, out float lastPlayedTime))
        {
            if (Time.unscaledTime - lastPlayedTime < soundCooldown)
            {
                return;
            }
        }

        if (randomPitch)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
        }

        clipPlayTimestamps[audioSource.clip] = Time.unscaledTime;

        audioSource.Play();
    }
}
