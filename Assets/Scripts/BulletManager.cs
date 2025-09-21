using System;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float maxRange;
    private float range = 0;
    public float spawnDist;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.Translate(Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = speed * Time.deltaTime;
        if (maxRange > range )
        {
            transform.Translate(Vector3.forward * dist);
            range += dist;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide with :" + other.gameObject.name);
        if (other.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            //collision.gameObject.SendMessage("bulletHit");
        }    }
    
}
