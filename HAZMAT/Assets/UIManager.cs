using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool IsUIOpen = false;

    //key press checks
    public GameObject mediaPlayerUI, inventoryUI, settingsUI, mapUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleUI(mediaPlayerUI);
        if (Input.GetKeyDown(KeyCode.I)) ToggleUI(inventoryUI);
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleUI(settingsUI);
        if (Input.GetKeyDown(KeyCode.N)) ToggleUI(mapUI);
        
    }

    public void ToggleUI(GameObject uiElement)
    {
        bool isActive = !uiElement.activeSelf;
        uiElement.SetActive(isActive);
        IsUIOpen = isActive; // ✅ Update global state
    }
}
