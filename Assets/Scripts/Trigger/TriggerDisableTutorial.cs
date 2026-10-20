using UnityEngine;

public class TriggerDisableTutorial : MonoBehaviour
{
    /* Alexis Clay Drain */
    
    public void DisableMoveTutorial() {
        GameManager.tutorial_Move.SetActive(false);
        GameManager.tutorial_Controller.SetActive(false);
    }
}
