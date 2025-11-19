using Photon.Pun;
using PlayerClass;
using PlayerClass.EpitaGame.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandlerTeam : MonoBehaviour
{
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;
    private Team _teamControl;
    private void Start()
    {
        GameObject[] player =  GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].gameObject.GetComponent<Player>() != null &&player[i].gameObject.GetComponent<PhotonView>() != null &&
                 player[i].GetPhotonView().IsMine)
            {
                _teamControl = player[i].GetComponent<Player>().GetTeam();
            }
        }
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

    }


    public void HealthChanged()
    {
        float healthRatio = (float)_teamControl.HealthPoints / _teamControl.MaxHealthPoints;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}