using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject _Crosshair;
    public GameObject BreakMenu;
    public GameObject DieMenu;
    public static Boolean InBreak;
    void Start()
    {
        InBreak = false;
        _Crosshair = GameObject.Find("Crosshair");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


            InBreak = !InBreak;
            _Crosshair.SetActive(!_Crosshair.activeSelf);
            BreakMenu.SetActive(!BreakMenu.activeSelf);
            
            if (InBreak)
            {
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("menu");
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
