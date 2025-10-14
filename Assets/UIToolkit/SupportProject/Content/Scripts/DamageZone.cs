using System;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int Damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        var playerControl = other.gameObject.GetComponentInParent<PlayerControl>();

        if (playerControl != null)
        {
            playerControl.ChangeHealth(-Damage);
        }
    }
}
