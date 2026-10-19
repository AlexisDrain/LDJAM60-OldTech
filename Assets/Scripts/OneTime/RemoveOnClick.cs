using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveOnClick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update() {
        if (GameManager.playerActionsControls.UI.Click.WasPressedThisFrame()) {
            DestroyObject();
            GameManager.button_StartGame.GetComponent<Selectable>().Select();
        }
    }
    public void SelectStartButton() {
        GameManager.button_StartGame.GetComponent<Selectable>().Select();
    }
    // Update is called once per frame
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
