using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 _playerVelocity;
    private CharacterController _controller;

    public float jumpHeight;
    public float speed ;
    public float mouseSensitivity ;
    
    private float Xrotation;

    public BulletManager bullet;
    
    private PhotonView _photonView;
    public GameObject camera;

    
    void Start()
    {
        Xrotation = 0f;
        _playerVelocity = new Vector3(0,0,0);
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //_camera = Camera.main.gameObject;
        _photonView = GetComponent<PhotonView>();
        /*
        if (_photonView.IsMine)
        {
            _camera.transform.position = camera_head.transform.position;
            _camera.transform.rotation = camera_head.transform.rotation;
        }
        */
    }
    

    private void Update()
    {
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
        _controller.transform.Translate(newdir * speed * Time.deltaTime);
        
        if (!_controller.isGrounded)
        {
            _controller.Move(Physics.gravity * Time.deltaTime);
        }
        else
        {
            if (Input.GetButtonDown("Jump"))
            {
                _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
                _controller.Move(_playerVelocity * Time.deltaTime);
            }
        }
        //move y (camera)
        float v = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        //transform.Rotate(new Vector3(0, h, 0));
        Xrotation -= v;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);
        
        camera.transform.localRotation = Quaternion.Euler(Xrotation,0f,0f);
        //0.7f is 90 x in rotation unity
        if ( Mathf.Abs(Mathf.Clamp(v + camera.transform.rotation.x,-90f,90f) - 
                       (v + camera.transform.rotation.x)) > 0.1)
        {
            camera.transform.eulerAngles = new Vector3(90,0, 0);

        }
        else
        {
            camera.transform.Rotate(new Vector3(-v,0,0) );
        }
    }
    
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BulletManager instance = PhotonNetwork.Instantiate(bullet.name, camera.transform.position,
                camera.transform.rotation).gameObject.GetComponent<BulletManager>();
            instance.player = gameObject;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletManager other =collision.gameObject.gameObject.GetComponent<BulletManager>();
            if (other.player != gameObject)
            {
                _photonView.RPC("DestroyGameObject", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID);

            }
        }
    }
    
    
}
