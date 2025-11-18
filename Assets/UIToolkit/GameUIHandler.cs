using Photon.Pun;
using PlayerClass.EpitaGame.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public Character PlayerControl;
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;

    private void Start()
    {
        GameObject[] player =  GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < player.Length; i++)
        {
            if (player[i].GetPhotonView().IsMine)
            {
                PlayerControl = player[i].GetComponent<Character>();
            }
        }
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

        HealthChanged();
    }


    public void HealthChanged()
    {
        float healthRatio = (float)PlayerControl.HealthPoints / PlayerControl.MaxHealthPoints;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}