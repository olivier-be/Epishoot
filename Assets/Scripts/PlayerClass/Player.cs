using System;
using System.Collections;
using System.Collections.Generic;
using EpitaGame;
using Photon.Pun;
using PlayerClass.EpitaGame.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Player : MonoBehaviour
{
    private Vector3 _playerVelocity;

    public float jumpHeight;
    public float speed ;
    public float mouseSensitivity ;
    
    private float Xrotation;

    public BulletManager bullet;
    
    private PhotonView _photonView;
    [FormerlySerializedAs("camera")] public GameObject camera_pos;
    private GameManager gameManager;
    
    private Ray _raycastHit;
    public TypePlayer typePlayer;
    private Character PlayerCharacter;
    
    public GameObject gameUIHandlerGameObject;

    private GameUIHandler gameUIHandler;

    
    void Start()
    {
        PlayerCharacter = PlayerBuilder.Spawn(typePlayer);
        _photonView = GetComponent<PhotonView>();

        Xrotation = 0f;
        _playerVelocity = new Vector3(0,0,0);

        //_camera = Camera.main.gameObject;
        if (_photonView.IsMine)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        /*
        if (_photonView.IsMine)
        {
            _camera.transform.position = camera_head.transform.position;
            _camera.transform.rotation = camera_head.transform.rotation;
        }
        */
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); 
        gameUIHandler = gameManager.PlayerHealthBar.GetComponent<GameUIHandler>();
    }

    public void OnDestroy()
    {
        if (_photonView.IsMine)
        {
            gameManager.LoseMenu();
        }
    }


    private void Update()
    {
        _photonView = GetComponent<PhotonView>();

        if (!GameManager.InBreak && _photonView.IsMine)
        {
            move();
            shoot();
        }
    }

    void move()
    {
        // move x
        float h = mouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(new Vector3(0, h, 0));
        
        Vector3 newdir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(newdir * speed * Time.deltaTime);
        
        if (Input.GetButtonDown("Jump"))
        {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
                transform.Translate(_playerVelocity * Time.deltaTime);
        }
        //move y (camera)
        float v = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        //transform.Rotate(new Vector3(0, h, 0));
        Xrotation -= v;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);
        
        camera_pos.transform.localRotation = Quaternion.Euler(Xrotation,0f,0f);
        //0.7f is 90 x in rotation unity
        if ( Mathf.Abs(Mathf.Clamp(v + camera_pos.transform.rotation.x,-90f,90f) - 
                       (v + camera_pos.transform.rotation.x)) > 0.1)
        {
            camera_pos.transform.eulerAngles = new Vector3(90,0, 0);

        }
        else
        {
            camera_pos.transform.Rotate(new Vector3(-v,0,0) );
        }
    }
    
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _raycastHit =  Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
            if (Physics.Raycast(_raycastHit, out RaycastHit hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    PhotonView pv = hit.collider.gameObject.GetComponentInParent<PhotonView>();
                    
                    Debug.Log("player have :" + PlayerCharacter.HealthPoints);
                    _photonView.RPC("AttackPlayer", RpcTarget.All,_photonView.ViewID,pv.ViewID);
                    Debug.Log("player have :" + PlayerCharacter.HealthPoints);


 
                }
            }
            //BulletManager instance = PhotonNetwork.Instantiate(bullet.name, camera_pos.transform.position,
            //    camera_pos.transform.rotation,0).gameObject.GetComponent<BulletManager>();
            //instance.player = gameObject;
        }
    }

    [PunRPC]
    public void AttackPlayer (int viewID,int viewIDother)
    {
        gameUIHandler.HealthChanged();

        PhotonView targetPhotonView = PhotonView.Find(viewIDother);

        if (targetPhotonView.IsMine)
        {
            PhotonView other = PhotonView.Find(viewID);
            Character player = other.gameObject.GetComponentInParent<Player>().PlayerCharacter;
            Character playerattacked = targetPhotonView.gameObject.GetComponentInParent<Player>().PlayerCharacter;
            Debug.Log("player attacked have :" + playerattacked.HealthPoints);
            Debug.Log("player have :" + player.HealthPoints);

            player.Attack(playerattacked);
            Debug.Log("player attacked have :" + playerattacked.HealthPoints);
            Debug.Log("player have :" + player.HealthPoints);
            if (!playerattacked.IsAlive)
            {
                Debug.Log("Kill Player " + targetPhotonView.gameObject.tag);
                _photonView.RPC("DestroyGameObject", RpcTarget.All,viewIDother);               
            }

        }
    }

    [PunRPC]
    public void DestroyGameObject(int viewID)
    {
        PhotonView targetPhotonView = PhotonView.Find(viewID);

        if (targetPhotonView != null && targetPhotonView.IsMine)
        {
            PhotonNetwork.Destroy(targetPhotonView);
        }
    }


    
}
