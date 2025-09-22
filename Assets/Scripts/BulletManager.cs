using System;
using Photon.Pun;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;

    public float speed;
    public float maxRange;
    private float range = 0;
    public float spawnDist;
    public float damage;
    private PhotonView _photonView;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _photonView = GetComponent<PhotonView>();

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

        /*
        if (other.gameObject != player &&  
            (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) )
        {
            //player.gameObject.SendMessage("HitEnemy");
        }
        */
        _photonView.RPC("DestroyGameObject", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID);

    }
    
}
