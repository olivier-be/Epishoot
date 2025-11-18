using Photon.Pun;
using PlayerClass.EpitaGame.Models;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;

    private void Start()
    {
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

    }


    public void HealthChanged(Character PlayerControl)
    {
        float healthRatio = (float)PlayerControl.HealthPoints / PlayerControl.MaxHealthPoints;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}