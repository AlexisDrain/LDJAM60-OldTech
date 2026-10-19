using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Sensitivity : MonoBehaviour
{
    /* Alexis Clay Drain */
    public bool mouse;
    public Slider slider;
    public TextMeshProUGUI text_VolumeAmount;
    public void IncreaseByPoint2() {
        /* OldRange = (OldMax - OldMin)
			NewRange = (NewMax - NewMin)
			NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin */
        slider.value += 0.2f;
        slider.value = Mathf.Clamp(slider.value, 0.001f, 3f);

        UpdateValues();
    }
    public void DecreaseByPoint2() {
        /* OldRange = (OldMax - OldMin)
			NewRange = (NewMax - NewMin)
			NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin */
        slider.value -= 0.2f;
        slider.value = Mathf.Clamp(slider.value, 0.001f, 3f);

        UpdateValues();
    }
    private void UpdateValues() {
        float newMixerValue = slider.value;

        text_VolumeAmount.text = slider.value.ToString("0.00");
        if (mouse) {
            GameManager.mouseSensitivity = newMixerValue;
        } else {
            GameManager.keyboardSensitivity = newMixerValue;
        }
    }
}
