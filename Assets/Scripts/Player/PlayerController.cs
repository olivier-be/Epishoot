using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 _playerVelocity;
    private CharacterController _controller;

    public float jumpHeight;
    public float speed ;
    private GameObject _camera;
    public float mouseSensitivity ;
    
    private float Xrotation;

    public BulletManager bullet;

    
    void Start()
    {
        Xrotation = 0f;
        _playerVelocity = new Vector3(0,0,0);
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        _camera = gameObject.GetComponentInChildren<Camera>().gameObject;
    }
    

    private void Update()
    {
        if (!EscapeManager.InBreak)
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
        
        _camera.transform.localRotation = Quaternion.Euler(Xrotation,0f,0f);
        //0.7f is 90 x in rotation unity
        if ( Mathf.Abs(Mathf.Clamp(v + _camera.transform.rotation.x,-90f,90f) - 
                       (v + _camera.transform.rotation.x)) > 0.1)
        {
            _camera.transform.eulerAngles = new Vector3(90,0, 0);

        }
        else
        {
            _camera.transform.Rotate(new Vector3(-v,0,0) );
        }
    }
    
    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            BulletManager instance = Instantiate(bullet, _camera.transform.position,
                _camera.transform.rotation);
            instance.player = gameObject;
        }
    }
    
    
}
