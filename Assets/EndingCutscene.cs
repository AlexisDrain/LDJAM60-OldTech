using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCutscene : MonoBehaviour
{
    public TypeDialogue typeDialogue;
    public Animator myAnimator;
    public Button button_restartGame;
    
    void Start()
    {
        button_restartGame.gameObject.SetActive(false);
    }
    public void InvokeEnding() {
        gameObject.SetActive(true);
        StartCoroutine("CountdownEnding");
    }
    private IEnumerator CountdownEnding() {
        // GameManager.reduceMusicVolumeHotlineMiami = true;
        myAnimator.SetTrigger("StartEnding");
        yield return new WaitForSecondsRealtime(2f);
        typeDialogue.CreateNewText(4);
        yield return new WaitForSecondsRealtime(8f);
        button_restartGame.gameObject.SetActive(true);
    }
}
