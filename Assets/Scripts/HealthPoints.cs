using UnityEngine;
using System.Collections;

public class HealthPoints : MonoBehaviour
{
    public delegate void Chainge(int value);
    public Chainge OnChange = (i) => { };
    public int max = 1;

    public ParticleSystem blood;

    private int current;

    private void Start()
    {
        current = max;
    }

    public void Damage(int value)
    {
        current -= value;
        if (blood != null)
        {
            blood.Play();
        }
        if (current <= 0)
        {
            current = 0;
        }
        OnChange(current);
    }
}
