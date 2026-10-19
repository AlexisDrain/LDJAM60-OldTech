using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class ScrollBarControls : MonoBehaviour {
    /* Alexis Clay Drain */

    public RectTransform scrollView;
    public Scrollbar scrollBar;
    public TMP_InputField textField;
    public float speedMultiplier = 1000f;

    private Vector2 navControls;
    public void Update() {
        navControls = GameManager.playerActionsControls.UI.Navigate.ReadValue<Vector2>();

        // if (navControls.y > 0.1f && scrollView.anchoredPosition.y > 0f) {
        if ((navControls.y < -0.1f && scrollBar.value < 1f) || (navControls.y > 0.1f && scrollBar.value > 0f)) {
            scrollView.anchoredPosition = new Vector2(scrollView.anchoredPosition.x, scrollView.anchoredPosition.y + (-1f * navControls.y * speedMultiplier * Time.unscaledDeltaTime));

            // changing scrollBar.value is broken in Unity. Instead:
            // OnScroll() updates the scrollBar value and handle position.
            // The EventSystem data doesn't matter, this is basically a null input
            textField.OnScroll(new PointerEventData(GameManager.eventSystem));
            // textField.OnScroll(new PointerEventData(EventSystem.current));
        }
        
    }
    /*
    public override void OnMove(AxisEventData eventData) {
        // if (!IsActive() || !IsInteractable()) {
        print("move");
        if (eventData.moveVector.x < -0.5f) {
            myScrollbar.value -= 0.1f;
        } else if (eventData.moveVector.x > 0.5f) {
            myScrollbar.value += 0.1f;
        } else if (eventData.moveVector.y < -0.5f) {
            base.OnMove(eventData);
        } else if (eventData.moveVector.y > 0.5f) {
            base.OnMove(eventData);
        }
        return;
    }
    */
}
