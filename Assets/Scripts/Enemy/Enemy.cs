using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {

        GameObject other = collision.gameObject;
        Debug.Log("collide with :" + other.tag);

        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
