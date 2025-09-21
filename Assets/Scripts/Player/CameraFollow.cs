using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float mouseSensitivity ;
    float Xrotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Xrotation = 0f;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void Update()
    {
        //TODO remove
        /*
        if (Input.GetKey(KeyCode.Space))
            Cursor.lockState = CursorLockMode.None;
        */ 
        //float h = mouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;
        float v = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        //transform.Rotate(new Vector3(0, h, 0));
        Xrotation -= v;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);
        
        transform.localRotation = Quaternion.Euler(Xrotation,0f,0f);
        //0.7f is 90 x in rotation unity
        if ( Mathf.Abs(Mathf.Clamp(v + transform.rotation.x,-90f,90f) - 
            (v + transform.rotation.x)) > 0.1)
        {
            transform.eulerAngles = new Vector3(90,0, 0);

        }
        else
        {
            transform.Rotate(new Vector3(-v,0,0) );
        }
        

    }



    
    private void OnDestroy()
    {
        //Cursor.lockState = CursorLockMode.None;
    }
}
