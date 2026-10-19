using UnityEngine;

public class ChangeMatOnStart : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Material newMat;
    void Start() {
        GetComponent<Renderer>().material = newMat;
    }
}
