using Photon.Pun;
using PlayerClass.EpitaGame.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;
    public Character PlayerControl;
    private void Start()
    {
        SetTeam();
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");
        HealthChanged();

    }

    public void SetTeam()
    {
        GameObject[] player =  GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].gameObject.GetComponent<Player>() != null &&player[i].gameObject.GetComponent<PhotonView>() != null &&
                player[i].GetPhotonView().IsMine)
            {
                Player p = player[i].GetComponent<Player>();
                PlayerControl = p.GetCharacter();
                
            }
        }
        GameManager gm =  GameObject.FindFirstObjectByType<GameManager>();
        gm.PlayerHealthBar = gameObject;
    }

    public void HealthChanged()
    {
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

        if (PlayerControl == null)
        {
            SetTeam();
        }
        float healthRatio = (float)PlayerControl.HealthPoints / PlayerControl.MaxHealthPoints;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        if (m_HealthBarMask != null)
        {
            m_HealthBarMask.style.width = Length.Percent(healthPercent);
        }
    }
}