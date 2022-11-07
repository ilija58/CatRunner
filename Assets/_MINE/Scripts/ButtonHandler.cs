using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        GameBehaviour.ChangeGameTimeState(true);
    }
    public void HideMenu()
    {
        Canvas mainMenu = GameObject.Find("Menu").GetComponent<Canvas>();
        mainMenu.enabled = false;
    }

    // Update is called once per frame
    
}
