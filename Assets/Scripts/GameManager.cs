using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Crosshair;
    public GameObject BreakMenu;
    public GameObject DieMenu;
    public static Boolean InBreak;
    
    void Start()
    {
        InBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


            InBreak = !InBreak;
            Crosshair.SetActive(!Crosshair.activeSelf);
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
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("menu");
    }

    
    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public void Respawn()
    {
        PhotonNetwork.Instantiate("Player",new Vector3(0, 1, -10), Quaternion.identity, 0);
        InBreak = false;
        Cursor.lockState = CursorLockMode.Locked;
        Crosshair.SetActive(true);
        DieMenu.SetActive(false);
        
    }

    public void LoseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Crosshair.SetActive(false);
        DieMenu.SetActive(true);
    }
    
}
