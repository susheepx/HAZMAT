using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenManager : MonoBehaviour
{
    public Transform canvas; 
    void Start()
    {
        ShowPanel("MainMenuPanel");
    }

    public void ShowPanel(string panelName)
    {
        foreach (Transform panel in canvas) 
        {
            if (panel.gameObject.name != "Background") panel.gameObject.SetActive(panel.name == panelName);
        }
        
    }
}
