using Photon.Pun;
using PlayerClass.EpitaGame.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;
    private Character PlayerControl;
    private void Start()
    {
        GameObject[] player =  GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].gameObject.GetComponent<Player>() != null &&player[i].gameObject.GetComponent<PhotonView>() != null &&
                 player[i].GetPhotonView().IsMine)
            {
                PlayerControl = player[i].GetComponent<Player>().GetCharacter();
            }
        }
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

    }


    public void HealthChanged()
    {
        float healthRatio = (float)PlayerControl.HealthPoints / PlayerControl.MaxHealthPoints;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}