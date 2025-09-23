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
        if (_photonView.IsMine)
        {
            float dist = speed * Time.deltaTime;
            if (maxRange > range)
            {
                transform.Translate(Vector3.forward * dist);
                range += dist;
            }
            else
            {
                _photonView.RPC("DestroyGameObject", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_photonView.IsMine)
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

            //DestroyGameObject(_photonView.ViewID);
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
