using System;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float maxRange;
    private float range = 0;
    public float spawnDist;
    public float damage;
    public 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Translate(Vector3.forward * spawnDist);
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
        Debug.Log(" Bullet collide with :" + other.gameObject.tag);

        if (other.gameObject.tag == "Enemy")
        {
            //player.gameObject.SendMessage("HitEnemy");
        }
        Destroy(gameObject);

    }
    
}
