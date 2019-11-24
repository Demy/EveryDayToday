using UnityEngine;
using System.Collections;

public class HealthPoints : MonoBehaviour
{
    public delegate void Chainge(int value);
    public Chainge OnChange = (i) => { };
    public int max = 1;

    public ParticleSystem blood;

    public int current;

    private void Start()
    {
        current = max;
    }

    public void Change(int value)
    {
        current = Mathf.Min(max, value + current);
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

    public int GetValue()
    {
        return current;
    }
}
