using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SliderFloat : MonoBehaviour, ISelectHandler {
    public Slider slider;
    public TMP_InputField sliderInputField;
    public RectTransform sliderClickableArea;

    public bool wholeNumbers = false;
    public float defaultValue = 3f;
    public float minValue = 0.2f;
    public float maxValue = 20f;
    public float buttonIncrementValue = 1f;
    public float buttonDecrementValue = -1f;

    public AudioClip selectAudioClip;
    public AudioClip pressedAudioClip;
    public AudioClip cannotChangeValueClip;
    //public TextMeshProUGUI textValue; // old, without inputField

    public void Start()
    {
        slider.wholeNumbers = wholeNumbers;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        // slider.OnMove
        UpdateSliderValue(false);
        slider.onValueChanged.AddListener(delegate { UpdateSliderValue(true); });
        // done in SaveManager
        // UpdateMouseSenValue();
    }

    /*
     * disable or enable all onValueChanged() listeners
     * hack: changing slider.minValue or slider.maxValue calls onValueChanged(). So we disable the events first then re-enable them
     */
    private void ToggleAllListeners(bool newState) {
        int listenerCount = slider.onValueChanged.GetPersistentEventCount();
        for (int i = 0; i < listenerCount; i += 1) {
            if (newState == true) {
                slider.onValueChanged.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            } else {
                slider.onValueChanged.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
            }
        }
    }
    public void ResetOptions(bool notify = true) {

        //GetComponent<SliderFloatInteract>().clickableArea = sliderClickableArea;
        // hack: changing slider.minValue or slider.maxValue calls onValueChanged(). So we disable the events first then re-enable them
        ToggleAllListeners(false);
        slider.wholeNumbers = wholeNumbers;
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        if (notify) {
            ToggleAllListeners(true);
            slider.value = defaultValue;
            UpdateSliderValue(false);
        } else {
            slider.SetValueWithoutNotify(defaultValue);
        }
        if (sliderInputField) {
            sliderInputField.text = defaultValue.ToString("0.##");
        }
        ToggleAllListeners(true);
    }
    /*
    public void OnMove(AxisEventData eventData) {
        //Assigns the move direction and the raw input vector representing the direction from the event data.
        MoveDirection moveDir = eventData.moveDir;
        Vector2 moveVector = eventData.moveVector;

        //Displays the information in the console
        print(moveDir + ", " + moveVector);
        print(eventData);
    }*/
    public void Increment() {
        slider.Select();
        if (slider.value == slider.maxValue) {
            GameManager.soundManager.SpawnLoudAudio(cannotChangeValueClip);
            return; // this is so that we don't play an SFX
        }
        slider.value += Mathf.Abs(buttonIncrementValue); // this will call slider.onValueChanged() which will play an SFX
        slider.value = Mathf.Round(slider.value);

        // UpdateSliderValue(false);
    }
    public void Decrement() {
        slider.Select();
        if (slider.value == slider.minValue) {
            GameManager.soundManager.SpawnLoudAudio(cannotChangeValueClip);
            return; // this is so that we don't play an SFX
        }
        slider.value -= Mathf.Abs(buttonDecrementValue); // this will call slider.onValueChanged() which will play an SFX
        slider.value = Mathf.Round(slider.value);

        // UpdateSliderValue(false);
    }
    public void UpdateSliderValue(bool playSFX = false) {
        float newValue = Mathf.Clamp(slider.value, minValue, maxValue);
        newValue = (float)Math.Round(newValue, 2);
        slider.value = newValue;
        if(sliderInputField) {
            sliderInputField.text = newValue.ToString("0.##");
        }
        if (playSFX) {
            GameManager.soundManager.SpawnLoudAudio(pressedAudioClip);
        }
        GameManager.ApplyOptions();
        //if(textValue) {
        //    textValue.text = newValue.ToString("0.##");
        //}
        // GameManager.mainCameraTrans.GetComponent<MouseLookNewInputSystem>().mouseSensitivity = slider.value;
    }
    public void UpdateSliderValueFromTextfield() {
        slider.Select();
        float newValue = Mathf.Clamp(float.Parse(sliderInputField.text), minValue, maxValue);
        newValue = (float)Math.Round(newValue, 2);
        sliderInputField.text = newValue.ToString("0.##");
        slider.value = newValue; // this will call slider.onValueChanged() which will play an SFX

        // visual bugfix for when new value length very long
        sliderInputField.textComponent.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        sliderInputField.textComponent.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        sliderInputField.caretPosition = 0;

        // GameManager.soundManager.SpawnLoudAudio(pressedAudioClip);
        // GameManager.saveManager.ApplyOptions();
        // GameManager.mainCameraTrans.GetComponent<MouseLookNewInputSystem>().mouseSensitivity = slider.value;
        //if (textValue) {
        //    textValue.text = newValue.ToString("0.##");
        //}
    }
    public void OnSelect(BaseEventData eventData) {
        GameManager.soundManager.SpawnLoudAudio(selectAudioClip);
    }

}
