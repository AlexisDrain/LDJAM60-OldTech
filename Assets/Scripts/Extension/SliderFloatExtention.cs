using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderFloatExtention : Slider {

    private UI_SliderFloat mySliderFloat;
    private new void Awake() {
        mySliderFloat = GetComponent<UI_SliderFloat>();
        base.Awake(); // call Awake() in Slider
    }
    // public RectTransform clickableArea;
    public override void OnPointerDown(PointerEventData eventData) {
        RectTransform clickableArea = mySliderFloat.sliderClickableArea;
        Vector2 localMousePosition = clickableArea.InverseTransformPoint(Input.mousePosition);
        if (clickableArea.rect.Contains(localMousePosition)) {
            mySliderFloat.UpdateSliderValue(false);
            base.OnPointerDown(eventData);
        }
        Select();
    }
    public override void OnPointerUp(PointerEventData eventData) {
        RectTransform clickableArea = mySliderFloat.sliderClickableArea;
        Vector2 localMousePosition = clickableArea.InverseTransformPoint(Input.mousePosition);
        if (clickableArea.rect.Contains(localMousePosition)) {
            mySliderFloat.UpdateSliderValue(false);
            base.OnPointerUp(eventData);
        }
        Select();
    }
    public override void OnDrag(PointerEventData eventData) {
        RectTransform clickableArea = mySliderFloat.sliderClickableArea;
        Vector2 localMousePosition = clickableArea.InverseTransformPoint(Input.mousePosition);
        if (clickableArea.rect.Contains(localMousePosition)) {
            mySliderFloat.UpdateSliderValue(false);
            base.OnDrag(eventData);
        }
    }
    public override void OnMove(AxisEventData eventData) {
        // if (!IsActive() || !IsInteractable()) {
        
        if(eventData.moveVector.x < -0.5f) {
            mySliderFloat.Decrement();
        } else if (eventData.moveVector.x > 0.5f) {
            mySliderFloat.Increment();
        } else if(eventData.moveVector.y < -0.5f) {
            base.OnMove(eventData);
        } else if (eventData.moveVector.y > 0.5f) {
            base.OnMove(eventData);
        }
        return;
    }
}