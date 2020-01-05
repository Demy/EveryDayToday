using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    public delegate void Chainge(int value, int current);
    public Chainge OnChange = (i, j) => { };
    public int max = 1;

    public ParticleSystem blood;
    public ParticleSystem heal;

    public int current;

    private void Start()
    {
        current = max;
    }

    public void Change(int value)
    {
        current = Mathf.Max(0, Mathf.Min(max, value + current));
        if (value < 0 && blood != null)
        {
            blood.Play();
        }
        if (value > 0 && heal != null)
        {
            heal.Play();
        }
        OnChange(value, current);
    }

    public int GetValue()
    {
        return current;
    }
}
