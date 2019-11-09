using UnityEngine;

public class CharacterActionsController : MonoBehaviour
{ 
    public HealthPoints hp;
    public Weapon weapon;

    private void Start()
    {
        hp.OnChange += Hp_OnChange;
    }

    private void OnDestroy()
    {
        hp.OnChange -= Hp_OnChange;
    }

    public void HitForward()
    {
        if (weapon != null)
        {
            weapon.Attack();
        }
    }

    private void Hp_OnChange(int hpLeft)
    {
        if (hpLeft <= 0)
        {
            Destroy(gameObject);
        }
    }
}
