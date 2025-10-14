using UnityEngine;
using UnityEngine.UIElements;

public class GameUIHandler : MonoBehaviour
{
    public PlayerControl PlayerControl;
    public UIDocument UIDoc;

    private VisualElement m_HealthBarMask;

    private void Start()
    {
        PlayerControl.OnHealthChange += HealthChanged;
        m_HealthBarMask = UIDoc.rootVisualElement.Q<VisualElement>("HealthBarMask");

        HealthChanged();
    }


    void HealthChanged()
    {
        float healthRatio = (float)PlayerControl.CurrentHealth / PlayerControl.MaxHealth;
        float healthPercent = Mathf.Lerp(8, 88, healthRatio);
        m_HealthBarMask.style.width = Length.Percent(healthPercent);
    }
}