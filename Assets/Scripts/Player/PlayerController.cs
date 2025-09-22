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
    
    public BulletManager bullet;

    
    void Start()
    {
        _playerVelocity = new Vector3(0,0,0);
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        
        foreach (var v in transform.GetComponentsInChildren<Transform>())
        {
            if (v.gameObject.tag == "MainCamera")
            {
                _camera = v.gameObject;

            }
        }
    }
    

    private void Update()
    {
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

        shoot();

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
