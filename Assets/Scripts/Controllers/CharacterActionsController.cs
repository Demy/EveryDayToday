using UnityEngine;
using static Enums;

public class CharacterActionsController : MonoBehaviour
{
    public HealthPoints hp;
    public Weapon weapon;

    public GameObject unarmed;

    [SerializeField]
    private bool isArmed;
    public bool IsArmed { get => isArmed; protected set => isArmed = value; }

    private void Start()
    {
        hp.OnDeath += Hp_OnDeath;

        if (unarmed != null && !IsArmed) SwitchUnarmed(true);
    }

    public void SwitchUnarmed(bool isOn)
    {
        IsArmed = !isOn;
        if (unarmed)
            unarmed.SetActive(isOn);
        if (weapon)
            weapon.gameObject.SetActive(!isOn);
    }

    private void OnDestroy()
    {
        hp.OnDeath -= Hp_OnDeath;
    }

    public void HitForward()
    {
        if (weapon != null)
        {
            weapon.Attack();
        }
    }

    public void ApplyEffect(StateChange change)
    {
        switch (change.type)
        {
            case StateChangeType.HP:
                hp.Change((int)change.value);
                break;
            case StateChangeType.Armed:
                SwitchUnarmed(change.value <= 0);
                break;
        }
    }

    private void Hp_OnDeath(HealthPoints hp)
    {
        Destroy(gameObject);
    }
}
