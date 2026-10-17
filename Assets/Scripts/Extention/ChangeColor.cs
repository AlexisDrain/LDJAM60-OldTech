using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ChangeColor : MonoBehaviour
{
    public Color currentColor;
    public bool onUpdate = false;

    private TextMeshProUGUI textMeshProUGUI;
    private Light componentLight;
    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;
    void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        componentLight = GetComponent<Light>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();

    }
    private void LateUpdate() {
        if (onUpdate) {
            UpdateColor(currentColor);
        }
    }

    public void SetNewColor(string colorCode) {
        Color newColor;
        if (ColorUtility.TryParseHtmlString(colorCode, out newColor)) {
            if (currentColor == newColor) { // remove redundant call
                return;
            }
            UpdateColor(newColor);
        } else {
            print("cannot parse color");
        }

    }
    private void UpdateColor(Color newColor) {
        currentColor = newColor;

        if (textMeshProUGUI) {
            textMeshProUGUI.color = newColor;
        }
        if (componentLight) {
            componentLight.color = newColor;
        }
        if (spriteRenderer) {
            spriteRenderer.color = newColor;
        }
        if (meshRenderer) {
            /* old. Only works for the first material
            Material materialInstance = new Material(GetComponent<MeshRenderer>().sharedMaterial);
            GetComponent<MeshRenderer>().material = materialInstance;
            materialInstance.color = newColor;
            */
            // new
            List<Material> newMaterials = new List<Material>();
            for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++) {
                newMaterials.Add(new Material(meshRenderer.sharedMaterials[i]));
                newMaterials[i].color = newColor;
            }
            meshRenderer.SetMaterials(newMaterials);
        }
    }
}
