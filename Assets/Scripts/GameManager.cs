using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Photon.Pun;
using PlayerClass;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Crosshair;
    public GameObject BreakMenu;
    public GameObject DieMenu;
    public GameObject HealthBar;
    public GameObject PlayerHealthBar;
    public GameObject TeamHealthBar;
    public GameObject CrossHit;
    public GameObject TeamKill;
    public GameObject PlayerKill;
    public GameObject Slider;
    public Slider SliderSenis;


    private bool hit;

    
    public PhotonView _photonView;

    private Team Assistant;
    private Team Student;
    
    public int Sensibility;


    
    public static Boolean InBreak;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        InBreak = false;
        Assistant = new Team("Assistant",0);
        Student = new Team("Student",1);
        _photonView = GetComponent<PhotonView>();
        _photonView.RPC("CallOwner", RpcTarget.MasterClient);
        Sensibility = (int)SliderSenis.value;
    }

    // Update is called once per frame
    void Update()
    {
        Sensibility = (int)SliderSenis.value;

        if (Input.GetKeyDown(KeyCode.Escape) && !DieMenu.activeSelf)
        {


            InBreak = !InBreak;
            HealthBar.SetActive(!HealthBar.activeSelf);

            Crosshair.SetActive(!Crosshair.activeSelf);
            BreakMenu.SetActive(!BreakMenu.activeSelf);
            Slider.SetActive(!Slider.activeSelf);
            if (InBreak)
            {
                Cursor.lockState = CursorLockMode.None;

            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            if (HealthBar.activeInHierarchy)
            {
                TeamHealthBar = FindAnyObjectByType<GameUIHandlerTeam>().gameObject;
                TeamHealthBar.GetComponent<GameUIHandlerTeam>().SetTeam();
                TeamHealthBar.GetComponent<GameUIHandlerTeam>().HealthChanged();

                PlayerHealthBar =  FindAnyObjectByType<GameUIHandler>().gameObject;
                PlayerHealthBar.GetComponent<GameUIHandler>().SetTeam(); 
                PlayerHealthBar.GetComponent<GameUIHandler>().HealthChanged();
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

    public void Respawn(string prefabname)
    {
        PhotonNetwork.Instantiate(prefabname,new Vector3(0, 1, -10), Quaternion.identity, 0);
        InBreak = false;
        Cursor.lockState = CursorLockMode.Locked;
        Crosshair.SetActive(true);
        HealthBar.SetActive(true);
        Crosshair.SetActive(true);
        
        Slider.SetActive(false);

        DieMenu.SetActive(false);
        TeamKill.SetActive(false);
        PlayerKill.SetActive(false);


        
    }

    public void LoseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        BreakMenu.SetActive(false);
        HealthBar.SetActive(false);
        Crosshair.SetActive(false);
        DieMenu.SetActive(true);
        Slider.SetActive(true);

        if (!TeamKill.activeInHierarchy)
        {
            PlayerKill.SetActive(true);
        }

    }
    
    [PunRPC]
    public static void DestroyRPC(GameObject obj)
    {
        PhotonView photonView = obj.GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(obj);
        }
    }

    public Team GetAssistant()
    {
        return Assistant;
    }

    public Team GetStudent()
    {
        return Student;
    }

    [PunRPC]
    public void CallOwner()
    {
        _photonView.RPC("UpdateTeam", RpcTarget.All, Assistant.HealthPoints,Student.HealthPoints);               
    }
    
    [PunRPC]
    public void UpdateTeam(int teamAssistant, int teamStudent)
    {
        Assistant.setLife(teamAssistant);
        Student.setLife(teamStudent);
    }
    
    
    [PunRPC]
    public void UpdateTeamLife(int TeamID, int Life)
    {
        if (Assistant.Id == TeamID)
        {
            Debug.Log(Assistant.Name + " : " + Life);
            Assistant.setLife(Life);

        }
        else
        {
            Debug.Log(Student.Name + " : " + Life);

            Student.setLife(Life);
        }
        if (Life == 0)
        {
            TeamKill.SetActive(true);
            FindAnyObjectByType<Player>().GetComponent<PhotonView>().RPC("TeamKill", RpcTarget.All,TeamID);
            if (Assistant.Id == TeamID)
            {
                Assistant.setLife(Assistant.MaxHealthPoints);

            }
            else
            {
                Student.setLife(Student.MaxHealthPoints);
            }
        }

    }

    public void SetFalse()
    {
        
        hit = false; 
        CrossHit.SetActive(false);
    }
    
    public void hitCrosshair()
    {
        if (!hit)
        {
            hit = true;
            CrossHit.SetActive(true);
            Invoke("SetFalse",0.5f); // disable after 5 seconds
            
        }
    }

}
