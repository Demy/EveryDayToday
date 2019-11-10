using System;
using UnityEngine;

public class CharacterActionsController : MonoBehaviour
{ 
    public HealthPoints hp;
    public Weapon weapon;

    public GameObject unarmed;

    private void Start()
    {
        hp.OnChange += Hp_OnChange;

        SwitchUnarmed(true);
    }

    public void SwitchUnarmed(bool isOn)
    {
        if (unarmed)
            unarmed.SetActive(isOn);
        if (weapon)
            weapon.gameObject.SetActive(!isOn);
    }

    public bool IsUnarmed()
    {
        return unarmed.activeSelf;
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
