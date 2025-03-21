using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthSlider;

    public void DamageHealth(float damage)
    {
        healthSlider.value -= damage;
    }

    public void GainHealth(float health)
    {
        healthSlider.value += health;
    }
}
